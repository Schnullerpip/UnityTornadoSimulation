using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TornadoScript : MonoBehaviour
{

    public float springForce, damper, maxDistance, minDistance;
    public float tornadoStrength = 20;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

	// Use this for initialization
	void Start ()
	{
        rotationAxis.Normalize();
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
}
