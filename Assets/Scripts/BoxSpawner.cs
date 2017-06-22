using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

	public GameObject prefab;
	public float spawnRate = 0.5f;

	public Vector2 range = new Vector2(30,30);

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Spawn",spawnRate,spawnRate);
	}

	void Spawn () 
	{
		Instantiate(prefab,transform.position + new Vector3(Random.Range(-range.x,range.x),0,Random.Range(-range.y,range.y)) ,Quaternion.identity);
	}
}
