using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class NormalCalc : MonoBehaviour {

	public Transform obj;

	public Vector3 tornadoNormal = Vector3.up;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		tornadoNormal.Normalize();

		Vector3 direction = obj.position - transform.position;
		Vector3 projection = Vector3.ProjectOnPlane(direction,tornadoNormal);
		Vector3	normal = Quaternion.AngleAxis(90,tornadoNormal) * projection.normalized;

		Debug.DrawRay(transform.position,tornadoNormal * 100, Color.blue);
		
		
		Debug.DrawRay(transform.position,direction);
		
		Debug.DrawRay(transform.position,projection, Color.green);

//		Vector3 normallizedProj = projection.normalized;


	

		Debug.DrawRay(obj.position, normal * 4, Color.red);

		obj.position += normal*Time.deltaTime * 20;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);
	}
}
