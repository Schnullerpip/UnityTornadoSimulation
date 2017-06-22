using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class TornadoScript : MonoBehaviour
{

    [Tooltip("atributes, that configure the internal springs, that are attached to each caught object")]
    public float springForce, damper, maxDistance, minDistance;

    [Tooltip("the axis, that the caught objects will rotate around")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    public CapsuleCollider tornadoCollider;

    [Tooltip("angle that is added to the object's velocity (higher lift -> quicker on top)")]
	[Range(0,90)]
    public float lift;

    [Tooltip("the force that will drive the caught objects around the tornados center")]
    public float tornadoStrength;

	// Use this for initialization
	void Start ()
	{
        //normalize the rotation axis given by the user
        rotationAxis.Normalize();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody) return;
        if (other.attachedRigidbody.isKinematic) return;

        Caught caught = other.GetComponent<Caught>();
        if (!caught)
        {
            caught = other.gameObject.AddComponent<Caught>();
            caught.Init(this, springForce, damper, maxDistance, minDistance);
        }
        else if (caught && !caught.enabled)
        {
            caught.Init(this, springForce, damper, maxDistance, minDistance);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Caught caught = other.GetComponent<Caught>();
        if (caught)
        {
            caught.Release();
        }
    }

    public float GetStrength()
    {
        return tornadoStrength;
    }

    //the axis the caught objects rotate around
    public Vector3 GetRotationAxis()
    {
        return rotationAxis;
    }

    //Editor convenience
    void Reset()
    {
        if (!tornadoCollider)
        {
            tornadoCollider = gameObject.GetComponent<CapsuleCollider>();
            tornadoCollider.isTrigger = true;
        }
    }
}
