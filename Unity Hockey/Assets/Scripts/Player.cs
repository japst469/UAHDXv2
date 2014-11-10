using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string Email;
    public int Score = 0;
    public Material Color;
    public GameObject Paddle;
    public GameObject PaddlePrefab;
    
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

    /// <summary>
    /// Use this to move the Paddle with a collision with the mouse and the PlayerField.
    /// </summary>
	public void MovePaddleWithMouse () {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, (1 << 8)))
        {
            // Send the Paddle to the position where the mouse ray collides with the game board.
            Paddle.GetComponent<PaddleController>().MoveTo(_hit.point);
        }
	}

    /// <summary>
    /// Use this to move the Paddle as an Artificial Intelligence.
    /// </summary>
    public void MovePaddleWithAI () {

    }
}
