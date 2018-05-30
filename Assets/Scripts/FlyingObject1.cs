using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject1 : MonoBehaviour {
	private Rigidbody rig;
	private PlayerController player;
	public float speed = 2000.0f;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		rig.velocity = rig.transform.forward * speed * Time.deltaTime;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Wall" || col.tag == "Player") {
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if (hit.collider.tag == "Wall" || hit.collider.tag == "Player") {
					Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
					float rot = 90 - Mathf.Atan2 (reflect.z, reflect.x) * Mathf.Rad2Deg;
					int rotInt = Mathf.RoundToInt (rot);
					transform.rotation = Quaternion.LookRotation(reflect);
					rig.velocity = rig.transform.forward * speed * Time.deltaTime;
				}
			}
			if (col.tag == "Player") {
				player.SendMessage ("DealDamage", 1);
			}
		}
	}
}
