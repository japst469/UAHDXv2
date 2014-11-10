//Script does opening main menu stuff, related to intro sounds.
//@Supposed to RUNS ONCE PER BOOT, but doesn't!

using UnityEngine;
using System.Collections;

public class InitTitle : MonoBehaviour {
    
    //@TODO: Add runonce instance, so it only plays on 1st boot!
    void Start () {
        //Play 3 sounds
        audio.PlayOneShot( Resources.Load("Audio/Environment/Buzzer") as AudioClip );
        audio.PlayOneShot( Resources.Load("Audio/Sounds/Announcer/Menus/Main/vUAHDX") as AudioClip );
        this.audio.Play();     //Play main menu music (Hockey Fever Loop)
	}
	
	// Update is called once per frame
	void Update () {
	    //NOP
	}

}
