using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundEnemies : MonoBehaviour {
	public float speed = 350.0f;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.parent.position, Vector3.up, speed * Time.deltaTime);
	}
}
