using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {

    private GameObject table;
    private __GameData gameData;

	// Use this for initialization
	void Start () {
        table = GameObject.Find("  GameData").GetComponent<__GameData>().Table;
        gameData = GameObject.Find("  GameData").GetComponent<__GameData>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    /// <summary>
    /// Determine what to do based on what the puck is colliding with.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter( Collision collision )
    {
        if( collision.collider.tag == "Goal" )
        {
            Debug.Log("A goal has been detected. The puck should be respawning. " + collider.name );
            // Which player does this goal belong to?
            int playerIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if( collider.name.Contains(i.ToString()) )
                    playerIndex = i;
            }
            // Increment the score of the last player who touched this puck
            gameData.Players[playerIndex].GetComponent<Player>().Score++;
            // Repawn the puck using the Table scripts RespawnPuck Method.
            table.GetComponent<Table>().RespawnPuck(this.gameObject);
        }

        if (collision.collider.tag == "Paddle")
        {
            Debug.Log("The puck has been hit by a paddle.");
            GetComponentInChildren<MeshRenderer>().material = collision.gameObject.GetComponentInChildren<MeshRenderer>().material;
        }

        if ( collision.collider.tag == "Field" )
        {
            this.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}
