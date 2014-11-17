using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{

    GameObject obj;  //GameObject script is attached to
    Vector3 pos;

    void Start()
    {
        obj = this.gameObject;  //Get this Gameobject
        pos = obj.transform.position;
    }

    void Update()
    {
        pos.x = obj.transform.position.x;
        pos.y = obj.transform.position.y;
        pos.y = pos.y + 0.05f * Time.deltaTime;
        pos.z = obj.transform.position.z;
        obj.transform.position = pos;
    }
}