using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
    private TornadoScript tornadoReference;
    private SpringJoint spring;
    private Rigidbody rigid;


	// Use this for initialization
	void Start ()
	{
	    rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        //lift spring so objects are pulled upwards
        //spring.connectedAnchor += tornadoReference.GetRotationAxis() * 10 * Time.deltaTime;
        Vector3 newPosition = spring.connectedAnchor;
        newPosition.y = transform.position.y;
        spring.connectedAnchor = newPosition;

	}

    void FixedUpdate()
    {
        //rotate object around tornado center


        //adjust spring strength relative to tornado distance
        //float distance = Vector3.Distance(transform.position, tornadoReference.transform.position);

        //spring.spring = tornadoReference.tornadoCollider.radius - Vector3.Distance(transform.position, tornadoReference.transform.position);// + (tornadoReference.minDistance);


        /*this is bad
        transform.RotateAround(tornadoReference.transform.position, tornadoReference.GetRotationAxis(), tornadoReference.GetTornadoStrength());*/

        //this is better
        Vector3 direction = transform.position - tornadoReference.transform.position;
        //project
        Vector3 projection = Vector3.ProjectOnPlane(direction, tornadoReference.GetRotationAxis());
        projection.Normalize();
	    Vector3 normal = Quaternion.AngleAxis(130, tornadoReference.GetRotationAxis()) * projection;
        normal = Quaternion.AngleAxis(tornadoReference.lift/* - Random.Range(-5.0f, 5.0f)*/, projection) * normal;
        Debug.DrawRay(transform.position, normal*10, Color.red);

        rigid.AddForce(normal*tornadoReference.GetStrength(), ForceMode.Force);
        
    }

    //call this when tornadoReference already exists
    public void Init(TornadoScript tornadoRef, float springForce, float damper, float maxDistance, float minDistance)
    {
        //make sure this s enabled (for reentrance)
        enabled = true;

        //save tornado reference
        tornadoReference = tornadoRef;

        //initialize the spring
        spring = gameObject.AddComponent<SpringJoint>();
        spring.spring = springForce;
        spring.damper = damper;
        spring.maxDistance = maxDistance;
        spring.minDistance = minDistance;
        spring.connectedBody = tornadoRef.gameObject.GetComponent<Rigidbody>();



        spring.autoConfigureConnectedAnchor = false;

        //set initial position of the caught object relative to its position and the tornado
        Vector3 initialPosition = Vector3.zero;
        initialPosition.y = transform.position.y;
        spring.connectedAnchor = initialPosition;

        //StartCoroutine(refreshImpulse());
    }

    public void Release()
    {
        enabled = false;
        Destroy(spring);
    }

    IEnumerator refreshImpulse()
    {
        transform.RotateAround(tornadoReference.transform.position, tornadoReference.GetRotationAxis(), tornadoReference.GetStrength());
        yield return new WaitForSeconds(0.5f);
    }
}
