using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class PowerUps : MonoBehaviour
{

    //Reference variables
    public __GameData GD;
    public GameObject Spawner;  //The gameobject of the powerup spawner anchor

    GameObject Powerup; //The powerup collectible gameobject. (This.gameobject)
    public float radius_X, radius_Z;    //+-x and +-z ranges to randomize within
    public Texture[] Tex = new Texture[19];   //The textures for each powerup
    __GameData.AirHockeyPowerups Pwr_Type = __GameData.AirHockeyPowerups.Nothing; //Create a new powerup type

    //Valid powerup choice in Air/Battle modes
    bool[] ValidAir =
    {false,true,false,true,true,
     true,true,true,true,true,
     true,true,true,true,true,
     true,true,true};

    //Valid powerup choices in Pinball mode
    bool[] ValidPin =
    {
        true,false,true,false,true,
        true,true,true,true,true,
        true,true,true,true,true,
        true,true,true
    };

    // Use this for initialization
    void Start()
    {
        GD = GameObject.Find("  GameData").GetComponent<__GameData>();
        Spawner = GameObject.Find("PowerupSpawn") as GameObject;    //@Modify this
        Powerup = this.gameObject;  //Get the powerup object itself
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Routine to randomize the powerup collectible's position and type
    void Randomize()
    {
        bool valid = false;   //Flag to determine if randomized powerup is valid for mode

        const float Pwr_Min = (float)(__GameData.AirHockeyPowerups.Nothing); //The minimum powerup to randomize.range
        const float Pwr_Max = (float)(__GameData.AirHockeyPowerups.Random); //The maximum powerup to randomize.range
        UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;   //Get new randomization seed from system time. Needs to be sync in netgames!

        //Loop until a valid powerup is gotten
        while (valid == false)
        {
            Pwr_Type = (__GameData.AirHockeyPowerups)((int)(UnityEngine.Random.Range(Pwr_Min, Pwr_Max)));
            if (GD.Mode == __GameData.AirHockeyMode.PinBall)
            {
                valid = ValidPin[(byte)Pwr_Type];
            }
            else
            {
                valid = ValidAir[(byte)Pwr_Type];
            }
        }

        //Set textures
        Powerup.renderer.material.mainTexture = Tex[(byte)Pwr_Type];
        GD.Table.GetComponent<Table>().RandomizeInRegion(Spawner, Powerup, radius_X, radius_Z);
    }
}