using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter( Collision collision )
    {
        if( collision.transform.tag == "Goal" )
        {
            Debug.Log("A goal has been detected. The puck should be respawning.");
        }

        if (collision.transform.tag == "Paddle")
        {
            Debug.Log("The puck has been hit by a paddle.");
        }
    }
}
