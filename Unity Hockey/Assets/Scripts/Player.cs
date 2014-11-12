using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string Email;
    public int Score = 0;
    public Material Color;
    public GameObject Paddle;
    public GameObject PaddlePrefab;
    public PlayerTypes PlayerType = PlayerTypes.Computer;
    
    // Dont need to modify these.
    private RaycastHit _hit;

    /// <summary>
    /// The Player object holds information related to a Player. Duh.
    /// </summary>
	public Player () {
        // Set to a bogus email.
        if (Email == null)
            Email = "anonymous@email.com";
	}

    void Update()
    {
        switch(PlayerType)
        {
            case PlayerTypes.Human:
                MovePaddleWithMouse();
                break;
            case PlayerTypes.Computer:
                MovePaddleWithAI();
                break;
            case PlayerTypes.Network:
                GetNetworkPlayerIntent();
                break;
        }
    }

    /// <summary>
    /// Use this to move the Paddle with a collision with the mouse and the PlayerField.
    /// </summary>
	public void MovePaddleWithMouse () {
        //Debug.Log("moving " + name + " with the mouse.");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, (1 << 8)))
        {
            // Send the Paddle to the position where the mouse ray collides with the PlayerField.
            Paddle.GetComponent<PaddleController>().MoveTo(_hit.point);
        }
	}

    /// <summary>
    /// Use this to move the Paddle as an Artificial Intelligence.
    /// </summary>
    public void MovePaddleWithAI () {
        //Debug.Log("moving " + name + " with the ai.");
    }

    /// <summary>
    /// Use this to move the paddle based on the values presented from the Networkview.
    /// </summary>
    public void GetNetworkPlayerIntent()
    {
        //Debug.Log("moving " + name + " with the netview.");
    }

    public enum PlayerTypes
    {
        Human,
        Computer,
        Network
    }
}
