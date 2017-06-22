using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class TornadoScript : MonoBehaviour
{

    public float springForce, damper, maxDistance, minDistance;
    public float tornadoStrength = 20;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public CapsuleCollider tornadoCollider;

	// Use this for initialization
	void Start ()
	{
	    tornadoCollider = GetComponent<CapsuleCollider>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody) return;

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

    public Vector3 GetRotationAxis()
    {
        return rotationAxis;
    }

    public float GetTornadoStrength()
    {
        return tornadoStrength;
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
