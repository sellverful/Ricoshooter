using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillinSpinnin: MonoBehaviour {
	public float rotationSpeed = 100f;
	/*public bool right = true;
	public bool left = false;
	public float movespeed = 100f;*/
	private PlayerController player;
	// Use this for initialization
	void Start () {
		//transform.Rotate (Vector3.up * rotationSpeed);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
		/*if (right) {
			transform.Translate (Vector3.forward * Time.deltaTime * movespeed);
		} else if (left) {
			transform.Translate (Vector3.back * Time.deltaTime * movespeed);
		}*/
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			player.SendMessage ("DealDamage", 1);
		}
		/*if (col.tag == "Wall") {
			if (right) {
				left = true;
				right = false;
			} else if (left) {
				left = false;
				right = true;
			}
		}*/
	}
}
