using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveTowards : MonoBehaviour
{
	public float speed = 0.001f;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		// if no target specified, assume the player
		if (target == null)
		{

			if (GameObject.FindWithTag("Destination") != null)
			{
				target = GameObject.FindWithTag("Destination").GetComponent<Transform>();
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
	public void SetSimpleTarget(Transform newTarget)
	{
		target = newTarget;
	}
}
