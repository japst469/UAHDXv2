using UnityEngine;
using System.Collections;

public class CameraRevolve : MonoBehaviour {

    public Transform target;
    public float speed;
     
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        float x = transform.position.x;
        float z = transform.position.z;
        float nx = x * Mathf.Cos(speed * Time.deltaTime) - z * Mathf.Sin(speed * Time.deltaTime);
        float nz = x * Mathf.Sin(speed * Time.deltaTime) + z * Mathf.Cos(speed * Time.deltaTime);

        transform.position = new Vector3(nx,transform.position.y,nz);
        transform.LookAt(Vector3.zero);
	}
}
