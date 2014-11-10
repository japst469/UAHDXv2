using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Table : MonoBehaviour {

    public GameObject Player1Spawn;
    public GameObject Player2Spawn;
    public GameObject Player3Spawn;
    public GameObject Player4Spawn;

   	// Use this for initialization
	void Awake () {
        Player1Spawn = GameObject.Find("P1PaddleSpawn") as GameObject;
        Player2Spawn = GameObject.Find("P2PaddleSpawn") as GameObject;
        Player3Spawn = GameObject.Find("P3PaddleSpawn") as GameObject;
        Player4Spawn = GameObject.Find("P4PaddleSpawn") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
