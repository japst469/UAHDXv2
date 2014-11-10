using UnityEngine;
using System.Collections;

/// <summary>
/// An object that handles all paddle inputs. To send the Paddle somewhere call: MoveTo(Vector3)
/// </summary>
public class PaddleController : MonoBehaviour {

    private Vector3 _target;
    private bool _changed = false;

	// Use this for initialization
	void Start () {
	    
	}

	void Update () {
        //rigidbody.MovePosition(_target);
        if (_changed)
        {
            rigidbody.MovePosition(_target);
            _changed = false;
        }
	}

    /// <summary>
    /// Tell the Paddle to move to the location.
    /// </summary>
    /// <param name="position">A vector where to send the Paddle.</param>
    public void MoveTo(Vector3 position)
    {
        // Simply set the target position.
        _target = position;
        _changed = true;
    }
}
