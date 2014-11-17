using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Portals : MonoBehaviour {

    //Referenece variables
    public __GameData GD;
    public GameObject PwrSpawn;
    public GameObject OtherPortal;
    GameObject Portal;

    //Local variables
    public float radius_X, radius_Z, height;

	// Use this for initialization
	void Start () {
        GD = GameObject.Find("  gameData").GetComponent<__GameData>();
        PwrSpawn= GameObject.Find("PowerupSpawn") as GameObject;    //@Modify this
        Portal = this.gameObject;
        GD.Table.GetComponent<Table>().RandomizeInRegion(PwrSpawn, Portal, radius_X, radius_Z);
	}

    void OnCollisionEnter(Collision otherObj)
    {
        Vector3 offset;

        //Get list of tags
        GameObject Pux=otherObj.gameObject;

        if (Pux.gameObject.tag=="Puck")
        {
            if (Portal.tag=="BluePortal")
            {
                OtherPortal= GameObject.Find("RedPortal") as GameObject;    //@Modify this
            }
            else
            {
                OtherPortal = GameObject.Find("BluePortal") as GameObject;    //@Modify this
            }

            offset.x=OtherPortal.transform.position.x;
            offset.y=OtherPortal.transform.position.y+height;
            offset.z=OtherPortal.transform.position.z;

            //Move and constrain object on Y, and with no rotation
            Pux.transform.position = offset;
            Pux.rigidbody.constraints = RigidbodyConstraints.None;
            Pux.rigidbody.useGravity = true;    //Disable gravity
            Portal.audio.clip = (AudioClip)(Resources.Load("Audio/Sounds/Powerups/Shared/Teleport"));
            Portal.audio.Play();
        }
    }
}
