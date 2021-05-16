using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		// if no target specified, assume the player
		if (target == null)
		{

			if (GameObject.FindGameObjectsWithTag("Destination").Length != 0)
			{
				target = GameObject.FindGameObjectsWithTag("Destination")[Random.Range(0, (GameObject.FindGameObjectsWithTag("Destination").Length-1))].GetComponent<Transform>();
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (target == null)
			return;

		// face the target
		transform.LookAt(target);

		//get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position, target.position);

		//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if (distance > minDist)
			transform.position += transform.forward * speed * Time.deltaTime;
	}

	// Set the target of the chaser
	public void SetTarget()
	{
		target =  GameObject.FindGameObjectsWithTag("Destination")[Random.Range(0, (GameObject.FindGameObjectsWithTag("Destination").Length-1))].GetComponent<Transform>();
	}
}
