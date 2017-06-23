using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheJointening : MonoBehaviour {

	public float breakforce = 30;
	public float overrideMass = 30;
	public float heightMulti = 7;

	// Use this for initialization
	void Start () 
	{
		Rigidbody[] children = GetComponentsInChildren<Rigidbody>();
		foreach (var rigid in children) 
		{

			rigid.mass = overrideMass;
			Collider[] neighbours = Physics.OverlapSphere(rigid.position,1);

			foreach (var n in neighbours) 
			{
				if(n.CompareTag("floor"))
					continue;
				if(n.attachedRigidbody == rigid)
					continue;

				FixedJoint spring = n.gameObject.AddComponent<FixedJoint>();
				spring.connectedBody = rigid;
				spring.breakForce = breakforce + Mathf.Pow(32 - n.transform.position.y, heightMulti);
				spring.breakTorque = breakforce + Mathf.Pow(32 - n.transform.position.y, heightMulti)  / 2;
			}
		}
 	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
