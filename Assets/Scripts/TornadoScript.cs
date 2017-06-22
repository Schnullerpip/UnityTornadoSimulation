using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TornadoScript : MonoBehaviour
{

    [SerializeField]
    private float springForce, damper, maxDistance, minDistance;
    [SerializeField]
    private float tornadoStrength = 20;
    [SerializeField]
    private Vector3 rotationAxis = new Vector3(0, 1, 0);

	// Use this for initialization
	void Start ()
	{
        rotationAxis.Normalize();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Caught>() && other.attachedRigidbody)
        {
            Caught caught = other.gameObject.AddComponent<Caught>();
            caught.Init(this, springForce, damper, maxDistance, minDistance);
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
