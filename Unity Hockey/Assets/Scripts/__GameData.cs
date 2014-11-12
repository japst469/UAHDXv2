﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class __GameData : MonoBehaviour
{

    public List<GameObject> Players;      // Store the local Player's information.
    public GameObject PlayerPrefab;     // Set this to a Player.
    public int PlayerCount = 4;         // The number of Players that the User selects.

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
        GameObject tableObject = null;      // used internally to store a temporary table struct.

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

        // Create a local array for the Goal Spawn Points.
        Vector3[] goalSpawnPosition = new Vector3[5];
        // set references to the spawn point.
        goalSpawnPosition[1] = tableObject.GetComponent<Table>().Player1Spawn.transform.position;
        goalSpawnPosition[2] = tableObject.GetComponent<Table>().Player2Spawn.transform.position;
        goalSpawnPosition[3] = tableObject.GetComponent<Table>().Player3Spawn.transform.position;
        goalSpawnPosition[4] = tableObject.GetComponent<Table>().Player4Spawn.transform.position;

        // Add a puck to the table.
        tableObject.GetComponent<Table>().AddPuck();
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
        if (Players.Count == PlayerCount && tableObject != null)
            initialized = true;
        else
            initialized = false;


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

    public enum AirHockeyNetType
    {
        Server,
        Client
    };
}

