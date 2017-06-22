using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoOuter : MonoBehaviour
{

    [SerializeField] private TornadoScript inner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        Vector3 pull = transform.position - other.transform.position;
        if (other.attachedRigidbody && pull.magnitude > inner.tornadoCollider.radius)
        {
            other.attachedRigidbody.AddForce(pull.normalized * (pull.magnitude + inner.tornadoCollider.radius), ForceMode.Force);
        }
    }
}