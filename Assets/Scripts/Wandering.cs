using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour {
	public float wanderRadius;
	public float wanderTimer;

	private Transform target;
	private NavMeshAgent agent;
	private float timer;
	private Vector3 newPos;
	// Use this for initialization
	void OnEnable () {
		agent = GetComponent<NavMeshAgent> ();
		timer = wanderTimer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= wanderTimer) {
			newPos = RandomNavSphere (transform.position, wanderRadius);
			agent.SetDestination (newPos);
			timer = 0;
		}
		Debug.DrawLine (transform.position, newPos,Color.cyan);
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist){
		Vector3 randDir = Random.insideUnitSphere * dist;
		randDir += origin;
		NavMeshHit navHit;

		NavMesh.SamplePosition (randDir, out navHit, dist,-1);
		return navHit.position;
	}
}
