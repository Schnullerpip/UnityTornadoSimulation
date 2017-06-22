using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
    private TornadoScript tornadoReference;
    private SpringJoint spring;


	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

        transform.RotateAround(tornadoReference.transform.position, tornadoReference.GetRotationAxis(), tornadoReference.GetTornadoStrength());


    }

    //call this when tornadoReference already exists
    public void Init(TornadoScript tornadoRef, float springForce, float damper, float maxDistance, float minDistance)
    {
        //save tornado reference
        tornadoReference = tornadoRef;

        //initialize the spring
        spring = gameObject.AddComponent<SpringJoint>();
        //spring.spring = springForce;
        //spring.damper = damper;
        spring.maxDistance = maxDistance;
        spring.minDistance = minDistance;
        spring.connectedBody = tornadoRef.gameObject.GetComponent<Rigidbody>();

        //StartCoroutine(refreshImpulse());
    }

    IEnumerator refreshImpulse()
    {
        transform.RotateAround(tornadoReference.transform.position, tornadoReference.GetRotationAxis(), tornadoReference.GetTornadoStrength());
        yield return new WaitForSeconds(0.5f);
    }
}
