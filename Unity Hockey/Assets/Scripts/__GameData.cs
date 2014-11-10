using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class __GameData : MonoBehaviour {

    public List<GameObject> Players;      // Store the local Player's information.
    public GameObject PlayerPrefab;     // Set this to a Player.

    public int PlayerCount = 4;         // The number of Players that the User selects.
    public AirHockeyMode Mode = AirHockeyMode.AirHockey;
    public AirHockeyDifficulty Difficulty = AirHockeyDifficulty.Easy;
    public AirHockeyState State = AirHockeyState.MainMenu;

    public double StateChange;

    private GameObject _go;
    private bool initialized;

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
        string name;
        Player player;
        GameObject tableObject = null;
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
                            tableObject = Instantiate(Resources.Load(name)) as GameObject;
                            if (null == tableObject)
                                Debug.LogError("Table Does Not Exist !!");
                            break;
                        case AirHockeyDifficulty.Medium:
                            {
                                Debug.Log(AirHockeyDifficulty.Medium.ToString());
                                name = PlayerCount > 2 ? "Prefabs/4P_Md" : "Prefabs/2P_Md";
                                tableObject = Instantiate(Resources.Load(name)) as GameObject;
                                if (null == tableObject)
                                    Debug.LogError("Table Does Not Exist !!");
                            }
                            break;
                        case AirHockeyDifficulty.Hard:
                            {
                                Debug.Log(AirHockeyDifficulty.Hard.ToString());
                                name = PlayerCount > 2 ? "Prefabs/4P_Lg" : "Prefabs/2P_Lg";
                                tableObject = Instantiate(Resources.Load(name)) as GameObject;
                                if (null == tableObject)
                                    Debug.LogError("Table Does Not Exist !!");
                            }
                            break;
                        default:
                            name = PlayerCount > 2 ? "Prefabs/4P_Sm" : "Prefabs/2P_Sm";
                            tableObject = Instantiate(Resources.Load(name)) as GameObject;
                            if (null == tableObject)
                                Debug.LogError("Table Does Not Exist !!");
                            break;
                    }

                }
                break;
            case AirHockeyMode.PinBall:
                {

                }
                break;
        }

        // Create an array for the Goal Spawn Points.
        Vector3[] goalSpawnPosition = new Vector3[5];
        // set eh aropriate spawn point.
        goalSpawnPosition[1] = tableObject.GetComponent<Table>().Player1Spawn.transform.position;
        goalSpawnPosition[2] = tableObject.GetComponent<Table>().Player2Spawn.transform.position;
        goalSpawnPosition[3] = tableObject.GetComponent<Table>().Player3Spawn.transform.position;
        goalSpawnPosition[4] = tableObject.GetComponent<Table>().Player4Spawn.transform.position;

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
            // Set the Players's Material to that of the Paddle that we just created. 
            //  This will be used later to change the Puck Color.
            player.Color = player.Paddle.GetComponentInChildren<Renderer>().material;

        }
        if (Players.Count == PlayerCount)
            initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        PinBall
    };
}

