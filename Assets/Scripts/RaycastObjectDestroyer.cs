using UnityEngine;
using System.Collections;

public class RaycastObjectDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Debug.DrawLine(transform.position, hit.point, Color.cyan);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}
