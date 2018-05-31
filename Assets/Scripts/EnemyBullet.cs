using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	public float speed;
	public int damage = 1;
	public float deathTimer = 5;
	Rigidbody rig;

	private PlayerController player;
	private float startingSpeed;
	// Use this for initialization


	void Start () {
		transform.Rotate (0, 0, 0);
		rig = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		startingSpeed = speed;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);	
		Destroy (gameObject, deathTimer);
		Reflect ();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !player.deflect) {
			player.SendMessage ("DealDamage", damage);
			Destroy (this.gameObject);
		}
	}
	void Reflect(){
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit, Time.deltaTime*speed)){
			if ((hit.collider.tag == "Player" && player.deflect) || hit.collider.tag == "Wall" || hit.collider.tag == "Enemy" || hit.collider.tag == "Box") {
				Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
				float rot = 90 - Mathf.Atan2 (reflect.z, reflect.x) * Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3 (0, rot, 0);
			}


		}
	}

	void FixedUpdate(){

	}
}
