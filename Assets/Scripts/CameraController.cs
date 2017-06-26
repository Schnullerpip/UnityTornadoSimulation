using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform followTarget;
	public Camera cam;

	public Vector3 centerOffset;

	public float zoomSpeed = 4;
	public float autoZoomSpeed = 100;

	public float startZoom = 0;

	private Vector3 camMovementVelocity;


	// Use this for initialization
	void Awake () 
	{
		Vector3 pos = cam.transform.localPosition;
		pos.z += startZoom;
		cam.transform.localPosition = pos;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(Input.GetKey(KeyCode.Z))
		{
			Vector3 pos = cam.transform.localPosition;
			pos.z -= Time.deltaTime * autoZoomSpeed;
			cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, pos, ref camMovementVelocity, Time.deltaTime);
		}

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
