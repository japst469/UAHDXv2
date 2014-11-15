using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Table : MonoBehaviour
{

    public GameObject Player1Spawn;
    public GameObject Player2Spawn;
    public GameObject Player3Spawn;
    public GameObject Player4Spawn;

    public GameObject Player1Goal;
    public GameObject Player2Goal;
    public GameObject Player3Goal;
    public GameObject Player4Goal;

    public GameObject PuckSpawn;
    public GameObject PowerupSpawn;


    private List<GameObject> pucks = new List<GameObject>();

    // Use this for initialization
    void Awake()
    {
        Player1Spawn = GameObject.Find("P1PaddleSpawn") as GameObject;
        Player2Spawn = GameObject.Find("P2PaddleSpawn") as GameObject;
        Player3Spawn = GameObject.Find("P3PaddleSpawn") as GameObject;
        Player4Spawn = GameObject.Find("P4PaddleSpawn") as GameObject;

        Player1Spawn = GameObject.Find("P1Goal") as GameObject;
        Player2Spawn = GameObject.Find("P2Goal") as GameObject;
        Player3Spawn = GameObject.Find("P3Goal") as GameObject;
        Player4Spawn = GameObject.Find("P4Goal") as GameObject;

        PuckSpawn = GameObject.Find("PuckSpawn") as GameObject;
        PowerupSpawn = GameObject.Find("PowerupSpawn") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Respawns the puck if it is outside of the bounds.
        foreach( GameObject go in pucks )
        {
            // The outside of bounds condition.
            if( OutsideBounds(go) )
            {
                // If its outside of the boundary then respawn it.
                RespawnPuck(go);
            }
        }
    }

    /// <summary>
    /// Determines if the puck is outside of the bounds.
    /// </summary>
    /// <returns>True if the puck is outside, otherwise false.</returns>
    public bool OutsideBounds( GameObject puck )
    {
        bool outOfBounds = (puck.transform.position.y < 2) ||
            (puck.transform.position.x > 20.0f) ||
            (puck.transform.position.x < -20.0f) ||
            (puck.transform.position.z > 20.0f) ||
            (puck.transform.position.z < -20.0f);

        return outOfBounds;
    }

    /// <summary>
    /// Respawns the puck at the PuckSpawn.
    /// </summary>
    /// <param name="go">The game object of the Puck.</param>
    public void RespawnPuck( GameObject go )
    {
        if( go != null )
        {
            // Make is stop moving.
            go.rigidbody.velocity = Vector3.zero;
            go.rigidbody.angularVelocity = Vector3.zero;
            // Reset its rotation.
            go.rigidbody.rotation = Quaternion.identity;
            // Reset its inertia. NOTE: may not be needed.
            //go.rigidbody.inertiaTensorRotation = Quaternion.identity;
            // reset its position.
            go.transform.position = PuckSpawn.transform.position;
            
        }
    }

    /// <summary>
    /// Creates a new puck at the Table's puckSpawn position.
    /// </summary>
    public void AddPuck()
    {
        Vector3 position = PuckSpawn.transform.position;
        AddPuck(position);
        //Debug.Log("Puck has been added.");
    }

    /// <summary>
    /// Creates a new puck at 'position'.
    /// </summary>
    /// <param name="position">The Vector3 where to put the new puck.</param>
    public void AddPuck(Vector3 position)
    {
        if (position == null)
            position = PuckSpawn.transform.position;

        GameObject go = GameObject.Find("Puck");
        if (go == null)
        {
            //Debug.Log("Did not find a puck in the scene, creating one from the prefab.");
            go = Instantiate(Resources.Load("Prefabs/PuckPrefab"), position, Quaternion.identity) as GameObject;
        }
        else
            Instantiate(go, position, Quaternion.identity);
        if (go != null)
            pucks.Add(go);

        go.name = "Puck";
        go.tag = "Puck";
    }

    /// <summary>
    /// Removes the specified puck from the list.
    /// </summary>
    /// <param name="go">The GameObject of the Puck.</param>
    public void RemovePuck( GameObject go )
    {
        // Remove the puck from the list of pucks and then destroy it's gameobject.
        if (pucks.Remove(go))
            Destroy(go);
        else
            Debug.Log("That puck doesn't exist.");
    }
}
