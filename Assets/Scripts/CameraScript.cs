using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject ObjectToFollow;
    public float smooth = 1f;

	void Start () {
        Vector3 CameraPosition;
        CameraPosition = transform.position;
        CameraPosition.x = ObjectToFollow.transform.position.x;
	}
	
	void FixedUpdate () {

        Vector3 CameraPosition = transform.position;
        CameraPosition.x = ObjectToFollow.transform.position.x;
        transform.position = Vector3.Lerp(transform.position, CameraPosition, 2);
        transform.LookAt(ObjectToFollow.transform);
	}
}
