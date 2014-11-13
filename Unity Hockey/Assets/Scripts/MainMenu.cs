using UnityEngine;
using System.Collections;

/// <summary>
/// This is basically the Entry Point for the Game, this is where the persistent GameData Object is created.
/// This script should be present on at least one camera in each scene.
/// </summary>
public class MainMenu : MonoBehaviour {
    public __GameData GameData;
    
    private GameObject gd;
    private RaycastHit hit;             //Raycast object
    private GameObject go;
    
	void Awake () {
        gd = GameObject.Find("  GameData");
        // Make sure the GameData object Exists.
        if (gd == null)
        {
            // If not create it.
            gd = Instantiate( Resources.Load("Prefabs/GlobalPrefab") as GameObject )as GameObject;
            gd.name = "  GameData";
        }
        // Cache the gamedata script
        GameData = gd.GetComponent<__GameData>() as __GameData;
        go = this.gameObject;
	}

    void Start()
    {
        string currentLevel = Application.loadedLevelName;
        switch (GameData.State)
        {
            case __GameData.AirHockeyState.MainMenu:
                
                break;
            case __GameData.AirHockeyState.PlayerSelect:
                
                break;
            case __GameData.AirHockeyState.OptionsMenu:
                
                break;
            case __GameData.AirHockeyState.NetGame:

                break;
            case __GameData.AirHockeyState.Game:
                if (!GameData.Initialized)
                        GameData.StartGame();
                
                break;
            case __GameData.AirHockeyState.Credits:
                
                break;
            default:
                Debug.LogError("State is in default. It should not be here!");
                break;
        }        
    }
    
    void Update()
    {
        // Cast a ray from the mouse's 3D position into the scene.
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, (1<<5)) )
        {
            // If a collision with A UI element has occured:
            string name = hit.transform.name;           //Get name of object raycasted
            bool clicked = Input.GetMouseButton(0);     // Was the Mouse clicked?
            string currentLevel = Application.loadedLevelName;
            // switch based on object's name
            switch (name)
            {
                case ("PlayerSelectOnePlayer"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.PlayerCount = 1;
                        //if (GameData.State == __GameData.AirHockeyState.PlayerSelect)
                            GameObject.Find("ArrowW").transform.position = new Vector3( -8.88f, -2.5f, -1.3f ) ;
                    }
                    break;
                case ("PlayerSelectTwoPlayer"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.PlayerCount = 2;
                        //if (GameData.State == __GameData.AirHockeyState.PlayerSelect)
                            GameObject.Find("ArrowW").transform.position = new Vector3(-7.2f, -2.5f, -1.3f);
                    }
                    break;
                case ("PlayerSelectThreePlayer"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.PlayerCount = 3;
                        //if (GameData.State == __GameData.AirHockeyState.PlayerSelect)
                            GameObject.Find("ArrowW").transform.position = new Vector3(-5.4f, -2.5f, -1.3f);
                    }
                    break;
                case ("PlayerSelectFourPlayer"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.PlayerCount = 4;
                        //if (GameData.State == __GameData.AirHockeyState.PlayerSelect)
                            GameObject.Find("ArrowW").transform.position = new Vector3(-3.45f, -2.5f, -1.3f);
                    }
                    break;
                case ("MainMenuOptionCollider"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.State = __GameData.AirHockeyState.MainMenu;
                        if ("MainMenu" != currentLevel)
                            Application.LoadLevel("MainMenu");
                    }
                    break;
                case ("PlayerSelectCollider"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.State = __GameData.AirHockeyState.PlayerSelect;
                        if ("PlayerSelect" != currentLevel)
                            Application.LoadLevel("PlayerSelect");
                        
                    }
                    break;
                case ("CreditsOptionCollider"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.State = __GameData.AirHockeyState.Credits;
                        if ("Credits" != currentLevel)
                            Application.LoadLevel("Credits");
                    }
                    break;
                case ("OptionsOptionCollider"):
                    if (clicked)
                    {
                        hit.transform.audio.Play();
                        GameData.State = __GameData.AirHockeyState.OptionsMenu;
                        if ("Options" != currentLevel)
                        {
                            Application.LoadLevel("Options");
                        }
                        
                    }
                    break;
                case ("PlayGameCollider"):
                    if (clicked)
                    {
                        GameData.State = __GameData.AirHockeyState.Game;
                        if ("Game" != currentLevel)
                            Application.LoadLevel("Game");
                    }
                    break;
                case ("QuitOptionsCollider"):
                    if (clicked)
                    {
                        Application.Quit();
                    }
                    break;
                //Debug button to skip EagleSoft Ltd FMV
                case ("Skip_FMV_1_Collider"):
                    if (clicked)
                    {
                        Application.LoadLevel("FMV2");
                    }
                    break;

                default:
                    if (clicked)
                    {
                        Debug.Log("You have selected an option from the menu that is not available yet.");
                    }
                    break;
            }

            if (clicked)
            {
                go.transform.audio.Play();
            }
        }
    }

}
