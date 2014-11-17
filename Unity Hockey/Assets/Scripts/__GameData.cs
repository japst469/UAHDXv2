using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class __GameData : MonoBehaviour
{

    public List<GameObject> Players;      // Store the local Player's information.
    public GameObject PlayerPrefab;     // Set this to a Player.
    public int PlayerCount = 4;         // The number of Players that the User selects.

    public GameObject Table;               // The current table in the scene.

    // The Game Mode determines how to load the Game Scene.
    public AirHockeyMode Mode = AirHockeyMode.AirHockey;
    // The Difficulty determines how to load the game scene and how the AI reacts to the player.
    public AirHockeyDifficulty Difficulty = AirHockeyDifficulty.Easy;
    // The State is a representation of where the game is at.
    public AirHockeyState State = AirHockeyState.MainMenu;

    private double StateChangeTime;         // The runtime when this variable was modified.
    private GameObject _go;                 // A cached object that this script is attached to.
    private bool initialized;               // Whether the Game Scene is properly initialized.


    void Awake()
    {
        // Make this object persist across all scenes.
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    public bool Initialized
    {
        get { return initialized; }
    }

    public void StartGame()
    {
        string name;                        // used internally to store several different names of objects.
        Player player;                      // used internally to store a temporary player struct.

        // Create the appropriate Table
        switch (Mode)
        {
            case AirHockeyMode.AirHockey:
                {
                    Debug.Log(AirHockeyMode.AirHockey.ToString());
                    switch (Difficulty)
                    {
                        case AirHockeyDifficulty.Easy:
                            Debug.Log(AirHockeyDifficulty.Easy.ToString());
                            name = PlayerCount > 2 ? "Prefabs/4P_Sm" : "Prefabs/2P_Sm";
                            Table = Instantiate(Resources.Load(name)) as GameObject;
                            if (null == Table)
                                Debug.LogError("Table Prefab Does Not Exist !!");
                            break;
                        case AirHockeyDifficulty.Medium:
                            {
                                Debug.Log(AirHockeyDifficulty.Medium.ToString());
                                name = PlayerCount > 2 ? "Prefabs/4P_Md" : "Prefabs/2P_Md";
                                Table = Instantiate(Resources.Load(name)) as GameObject;
                                if (null == Table)
                                    Debug.LogError("Table Prefab Does Not Exist !!");
                            }
                            break;
                        case AirHockeyDifficulty.Hard:
                            {
                                Debug.Log(AirHockeyDifficulty.Hard.ToString());
                                name = PlayerCount > 2 ? "Prefabs/4P_Lg" : "Prefabs/2P_Lg";
                                Table = Instantiate(Resources.Load(name)) as GameObject;
                                if (null == Table)
                                    Debug.LogError("Table Prefab Does Not Exist !!");
                            }
                            break;
                        default:
                            name = PlayerCount > 2 ? "Prefabs/4P_Sm" : "Prefabs/2P_Sm";
                            Table = Instantiate(Resources.Load(name)) as GameObject;
                            if (null == Table)
                                Debug.LogError("Table Prefab Does Not Exist !!");
                            break;
                    }

                }
                break;
            case AirHockeyMode.PinBall:
                {

                }
                break;
        }

        // Create a local array for the Goal Spawn Points.
        Vector3[] goalSpawnPosition = new Vector3[5];
        // set references to the spawn point.
        goalSpawnPosition[1] = Table.GetComponent<Table>().Player1Spawn.transform.position;
        goalSpawnPosition[2] = Table.GetComponent<Table>().Player2Spawn.transform.position;
        goalSpawnPosition[3] = Table.GetComponent<Table>().Player3Spawn.transform.position;
        goalSpawnPosition[4] = Table.GetComponent<Table>().Player4Spawn.transform.position;

        // Add a puck to the table.
        Table.GetComponent<Table>().AddPuck();
        Debug.Log("Puck has been added.");

        // Create The Players
        for (int i = 1; i <= PlayerCount; i++)
        {
            PlayerPrefab = Resources.Load("Prefabs/PlayerPrefab") as GameObject;
            // Create the number of Players we will need
            _go = Instantiate(PlayerPrefab) as GameObject;
            // Give the Player a useful name.
            _go.name = "Player " + i.ToString();
            Players.Add(_go);
            // cache the player object.
            player = _go.GetComponent<Player>();
            // cache the name for the prefab we want for this player.
            name = "Paddle" + i.ToString() + "Prefab";
            // Initialize the prefab.
            player.PaddlePrefab = Resources.Load("Prefabs/Paddles/" + name) as GameObject;
            // Since there is no paddle assigned to this player then go ahead and make one.
            player.Paddle = Instantiate(player.PaddlePrefab, goalSpawnPosition[i], Quaternion.identity) as GameObject;
            player.Paddle.name = "Paddle" + i.ToString();
            player.Paddle.tag = "Paddle";
            // Set the Players's Material to that of the Paddle that we just created. 
            //  This will be used later to change the Puck Color.
            player.Color = player.Paddle.GetComponentInChildren<Renderer>().material;
            // Make sure only player one is a human.
            // @TODO Add logic to add the network Player Type when there are network players.
            player.PlayerType = i == 1 ? Player.PlayerTypes.Human : Player.PlayerTypes.Computer;
        }

        // Determine if the game scene was properly initialized.
        if (Players.Count == PlayerCount && Table != null)
            initialized = true;
        else
            initialized = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for victory Condition.
        foreach( GameObject plr in Players )
        {
            if (plr.GetComponent<Player>().Score >= 10)
            {
                // @TODO add winner code.
                Debug.Log(plr.name + " has won!");
                
                break;
            }
        }
    }

    public void SendEmail()
    {
        byte[] score = new byte[4];
        score[0] = 5;
        score[1] = 10;
        score[2] = 10;
        score[3] = 10;

        string[] users = new string[4];
        users[0] = "Tamkis314";
        users[1] = "J0$hfinity";
        users[2] = "Mattyew14";
        users[3] = "GranTPain";

        //Replace to/from fields with approrpiate emails!
        const string to = "emailaddress@something.com,emailaddress@something.com,emailaddress@something.com,emailaddress@something.com";
        const string from = "emailaddress@something.com";
        string[] mail_creds = new string[2];
        mail_creds[0] = "emailaddress@something.com";            //email address of GMAIL sender
        mail_creds[1] = "FAKEPASSWORD";   //Password of GMAIL sender

        Email.Send(mail_creds, to, from, users, score);
    }

    public enum AirHockeyState
    {
        MainMenu,
        OptionsMenu,
        PlayerSelect,
        NetGame,
        Game,
        Credits
    }

    public enum AirHockeyDifficulty
    {
        Easy,
        Medium,
        Hard
    };
    public enum AirHockeyMode
    {
        AirHockey,
        PinBall,
        Battle
    };

    public enum AirHockeyNetType
    {
        Server,
        Client
    };

    public enum AirHockeyPowerups
    {
        Nothing,
        Army,
        BSaver,
        Big,
        Block,
        Bumpers,
        Fastmo,
        Grav,
        HotPot,
        Invis,
        Kill,
        Multi,
        Portals,
        Ramps,
        Shield,
        Slomo,
        Swap,
        Random
    };

}

