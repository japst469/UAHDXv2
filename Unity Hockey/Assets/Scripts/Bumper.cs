using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bumper: MonoBehaviour
{
    //Refernce variables
    public __GameData GD;
    public GameObject PwrSpawn;

    public float radius_X, radius_Z;
    GameObject bumper;    //this.gameobject

    // Use this for initialization
    void Start()
    {
        GD = GameObject.Find("  gameData").GetComponent<__GameData>();
        PwrSpawn = GameObject.Find("PowerupSpawn") as GameObject;    //@Modify this
        bumper= this.gameObject;
        GD.Table.GetComponent<Table>().RandomizeInRegion(PwrSpawn, bumper, radius_X, radius_Z);
    }

    void OnCollisionEnter(Collision otherObj)
    {
        byte r;
        GameObject Pux = otherObj.gameObject;

        if (Pux.tag=="Puck")
        {
            r = (byte)(UnityEngine.Random.Range(0, 10));
            if (r<=5)
            {
                bumper.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Shared/Bumper1"));
            }
            else
            {
                bumper.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Shared/Bumper2"));
            }
            bumper.audio.Play();
        }
    }
}
