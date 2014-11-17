using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wall_Piece : MonoBehaviour
{
    //Reference variables
    public __GameData GD;

    //Local object variables
    public GameObject Wall;    //this.gameobject
    private byte HP;

    // Use this for initialization
    void Start()
    {
        GD = GameObject.Find("  gameData").GetComponent<__GameData>();
        HP = 3;
        Wall = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Kill();
        }
    }

    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.transform.tag=="Puck")
        {
            if (GD.Mode==__GameData.AirHockeyMode.PinBall)
            {
                Wall.audio.clip=(AudioClip)(Resources.Load("Audio/Sounds/Powerups/Pinball/DeflectP_S"));
            }
            else
            {
                Wall.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Hockey/DeflectH_S"));
            }
            Wall.audio.Play();
            HP--;
        }
    }
    
    void Kill()
    {
        Destroy(Wall);
        Wall.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Shared/PwrUse.ogg"));
        Wall.audio.Play();
    }
}
