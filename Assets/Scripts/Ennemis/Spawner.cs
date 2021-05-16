using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject[] spawnPrefabs;

	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;

	private float savedTime;
	private float secondsBetweenSpawning;
	private Vector3 startingPosition;
	public float radius = 3f;

	// Use this for initialization
	void Start()
	{
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		startingPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - savedTime >= secondsBetweenSpawning) 
		{
			transform.position = new Vector3(Random.Range(radius, -radius), startingPosition.y, startingPosition.z);
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			transform.position = startingPosition;
		}
	}

	void MakeThingToSpawn()
	{
		int indexOfObjectToSpawn = Random.Range(0, spawnPrefabs.Length - 1);
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefabs[indexOfObjectToSpawn], transform.position, transform.rotation) as GameObject;

		if ((target != null) && (clone.gameObject.GetComponent<MoveTowards>() != null))
		{
			clone.gameObject.GetComponent<MoveTowards>().SetTarget();
		}
	}
}
