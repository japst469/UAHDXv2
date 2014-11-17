using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ramp : MonoBehaviour {

    public __GameData GD;
    public GameObject PwrSpawn; //Anchor point
    public float radius_X, radius_Z;    //Randomization for x/z movement
    GameObject ramp;    //This.gameobject

    // Use this for initialization
    void Start()
    {
        GD = GameObject.Find("  GameData").GetComponent<__GameData>();
        PwrSpawn = GameObject.Find("PowerupSpawn") as GameObject;
        ramp = this.gameObject;

        float yrot;
        GD.Table.GetComponent<Table>().RandomizeInRegion(PwrSpawn, ramp, radius_X, radius_Z);
        
        UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;   //@Get new randomization seed from system time. 
        yrot=UnityEngine.Random.Range(-360,360);
        ramp.transform.Rotate(270, yrot, 0);
    }

    void OnCollisionEnter(Collision otherObj)
    {
        GameObject Pux;
        Pux = otherObj.gameObject;

        if (otherObj.gameObject.tag=="Puck")
        {
            Pux.rigidbody.constraints = RigidbodyConstraints.None;
            Pux.rigidbody.useGravity= true;
            ramp.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Shared/Rampoff"));
            ramp.audio.Play();
        }
    }
}
