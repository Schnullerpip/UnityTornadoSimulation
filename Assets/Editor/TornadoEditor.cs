using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( TornadoScript ))]
public class TornadoEditor : Editor
{
	static bool visualize = true;

	static TornadoScript tornadoScript;

	public override void OnInspectorGUI(){
		DrawDefaultInspector();	
		visualize = EditorGUILayout.Toggle("Visualize", visualize);
	}

	void OnEnable()
	{
		if(tornadoScript == null)
			tornadoScript = target as TornadoScript;
	}

	[DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected)]
	static void DrawGizmos(TornadoScript tornado, GizmoType gizmoType)
	{
		if(!visualize)
			return;

		if( tornado == null || tornado.gameObject == null )
			return;

		Handles.color = Color.red;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.minDistance);

		Handles.color = Color.yellow;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.maxDistance);

		Handles.color = Color.blue;
		Handles.DrawLine(tornado.transform.position, tornado.transform.position + tornado.rotationAxis * 20);

		Handles.color = Color.green;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.GetComponent<CapsuleCollider>().radius);
	}

	void OnSceneGUI()
	{
		if(!visualize)
			return;
		
		Handles.color = Color.red;

		tornadoScript.maxDistance = Handles.ScaleValueHandle(tornadoScript.maxDistance,
			tornadoScript.transform.position + new Vector3(tornadoScript.maxDistance,0,0),
			Quaternion.identity,
			2,
			Handles.CubeHandleCap,
			2);

		Handles.color = Color.yellow;

		tornadoScript.minDistance = Handles.ScaleValueHandle(tornadoScript.minDistance,
			tornadoScript.transform.position + new Vector3(tornadoScript.minDistance,0,0),
			Quaternion.identity,
			2,
			Handles.CubeHandleCap,
			2);
		
		tornadoScript.maxDistance = Mathf.Max(tornadoScript.maxDistance,tornadoScript.minDistance);
	}
}
