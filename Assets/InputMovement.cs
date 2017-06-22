using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour {

	public float speed = 10;

	void Update()
	{
		Vector3 deltaPos = Vector3.zero;
		deltaPos.x = Input.GetAxis("Horizontal");
		deltaPos.z = Input.GetAxis("Vertical");
		transform.position += deltaPos * speed * Time.deltaTime;
	}
}
