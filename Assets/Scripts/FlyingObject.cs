using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour {
	private Rigidbody rig;
	public float speed = 400.0f;
	public Vector3 vel;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		if (GetComponent<BoxCollider> () != null) {
			//Physics.IgnoreCollision (GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag ("Plane").GetComponent<MeshCollider> ());
		} else if (GetComponent<SphereCollider> () != null) {
			
			Physics.IgnoreCollision (GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag ("Plane").GetComponent<MeshCollider>());
		}
		//rig.velocity = rig.transform.forward * 50 * Time.deltaTime;
	}
	// Update is called once per frame
	void Update () {
		vel = rig.velocity;
	}
	void OnTriggerEnter(Collider col){
		if (!enabled) return;
		Debug.DrawRay (transform.position, transform.forward);
		if (col.tag == "Wall") {
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if (hit.collider.tag == "Wall") {
					Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
					float rot = 90 - Mathf.Atan2 (reflect.z, reflect.x) * Mathf.Rad2Deg;
					int rotInt = Mathf.RoundToInt (rot);
					transform.rotation = Quaternion.LookRotation(reflect);
					rig.velocity = transform.forward * speed;
					Debug.Log ("Ricochete vel " + rig.velocity); 
				}
			}
		}
	}
}
