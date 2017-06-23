using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform followTarget;
	public Camera cam;

	public Vector3 centerOffset;

	public float zoomSpeed = 4;

	private Vector3 camMovementVelocity;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = followTarget.position;

		if(Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			Vector3 pos = cam.transform.localPosition;
			pos.z += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, pos, ref camMovementVelocity, Time.deltaTime);
		}

		cam.transform.LookAt(followTarget.position + centerOffset);

	}
}
