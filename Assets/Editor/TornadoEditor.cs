using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( TornadoScript ))]
public class TornadoEditor : Editor
{
	private TornadoScript tornado;

	void OnEnable()
	{
		if(tornado == null)
			tornado = target as TornadoScript;
	}

	void OnSceneGUI( )
	{
		if( tornado == null || tornado.gameObject == null )
			return;

		tornado.minDistance = CircleHandle(tornado.minDistance,Color.red);
		tornado.maxDistance = CircleHandle(tornado.maxDistance,Color.yellow);

		tornado.maxDistance = Mathf.Max(tornado.maxDistance,tornado.minDistance);

		Handles.color = Color.blue;
		Handles.DrawLine(tornado.transform.position, tornado.transform.position + tornado.rotationAxis * 20);
	}

	float CircleHandle(float value, Color color)
	{
		Handles.color = color;


		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, value);

		return Handles.ScaleValueHandle(value,
				tornado.transform.position + new Vector3(value,0,0),
				Quaternion.identity,
				2,
				Handles.CubeHandleCap,
				2);
	}
}
