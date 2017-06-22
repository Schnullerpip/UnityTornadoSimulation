using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( TornadoScript ))]
public class TornadoEditor : Editor
{
	static TornadoScript tornadoScript;


	void OnEnable()
	{
		if(tornadoScript == null)
			tornadoScript = target as TornadoScript;
	}

	[DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected)]
	static void DrawGizmos(TornadoScript tornado, GizmoType gizmoType)
	{
		Handles.color = Color.red;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.minDistance);

		Handles.color = Color.yellow;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.maxDistance);

		Handles.color = Color.blue;
		Handles.DrawLine(tornado.transform.position, tornado.transform.position + tornado.rotationAxis * 20);

		Handles.color = Color.green;
		Handles.DrawWireDisc(tornado.transform.position, Vector3.up, tornado.tornadoCollider.radius);

		Handles.color = new Color(0,0,1,0.3f);

		Handles.DrawSolidArc(tornado.transform.position,
			Vector3.left,
			Vector3.forward,
			tornado.lift, tornado.minDistance);
	}

	void OnSceneGUI()
	{
		if( tornadoScript == null || tornadoScript.gameObject == null )
			return;

		Undo.RecordObject(tornadoScript, tornadoScript.name+ " Changes");
		
		Handles.color = Color.yellow;
		tornadoScript.maxDistance = Handles.ScaleValueHandle(tornadoScript.maxDistance,
			tornadoScript.transform.position + new Vector3(tornadoScript.maxDistance,0,0),
			Quaternion.identity,
			2,
			Handles.CubeHandleCap,
			2);

		Handles.color = Color.red;
		tornadoScript.minDistance = Handles.ScaleValueHandle(tornadoScript.minDistance,
			tornadoScript.transform.position + new Vector3(tornadoScript.minDistance,0,0),
			Quaternion.identity,
			2,
			Handles.CubeHandleCap,
			2);
		
		tornadoScript.maxDistance = Mathf.Max(tornadoScript.maxDistance,tornadoScript.minDistance);

		Handles.color = new Color(0,0,1,0.3f);

		tornadoScript.lift = Handles.ScaleValueHandle(tornadoScript.lift,
			tornadoScript.transform.position + Quaternion.AngleAxis(tornadoScript.lift,Vector3.left) * Vector3.forward * tornadoScript.minDistance,
			Quaternion.identity,
			2,
			Handles.SphereHandleCap,
			2);

		tornadoScript.lift = Mathf.Clamp(tornadoScript.lift,0,90);
	}
}
