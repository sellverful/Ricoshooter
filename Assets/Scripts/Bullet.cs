using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed;
	public float speedOfFalling = 0.9f;
	public float timeOfFalling = 2f;
	public Gun gun;

	public Rigidbody rig;

	private PlayerController player;
	private bool ricocheted = false;
	private float time = 0f;
	private float startingSpeed;
	public float rotSpeed = 800f;
	// Use this for initialization
	bool once = true;
	[HideInInspector]public bool playersBullet = true;
	void Start () {
		rig = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		startingSpeed = speed;
		Destroy (gameObject, 5);
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		//Destroy (this.gameObject, 7);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		Destroy (this.gameObject, 5);
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit, Time.deltaTime*speed)){
			if (hit.collider.tag != "Bullet") {
				if (hit.collider.gameObject.tag == "Player" && player.deflect==true) {
					speed = startingSpeed;
				} 
				if (hit.collider.gameObject.tag == "Enemy" && playersBullet/*&& ricocheted*/) {
					if(hit.collider.GetComponent<enemy1>()!=null)
						hit.collider.GetComponent<enemy1>().SendMessage ("Die");
					if(hit.collider.GetComponent<enemy2>()!=null)
						hit.collider.GetComponent<enemy2>().SendMessage ("Die");
					Destroy (this.gameObject);
				}
				Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
				float rot = 90 - Mathf.Atan2 (reflect.z, reflect.x) * Mathf.Rad2Deg;
				int rotInt = Mathf.RoundToInt (rot);

				transform.position = hit.point;
				if (reflect.y != 0)
					reflect.y = 0;
				transform.rotation = Quaternion.LookRotation(reflect);
			}
		}

		//slowing bullet
		/*time -= Time.deltaTime;
		if (time < 0 && speed > 0) {
			time = timeOfFalling;
			speed *= speedOfFalling;
		}

		//stop bullet completelly
		if (speed < 1f) {
			speed = 0;
			rig.isKinematic = false;
			GetComponent<BoxCollider> ().enabled = true;
		}*/ // Old stopping bullet
	}

	void OnTriggerEnter(Collider col){
        if (!enabled) return;
		if (col.tag == "Player" && player.deflect == false) {
			player.SendMessage ("DealDamage", 1);
			Destroy (this.gameObject);
		} else if (col.tag == "Defence" && playersBullet) {
			playersBullet = false;
			if (col.GetComponent<ShieldDeflect> () != null)
				col.GetComponent<ShieldDeflect> ().SendMessage ("ReceiveDamage");
			GetComponent<Renderer> ().material.color = Color.red;
		} else if (col.tag == "Enemy" && playersBullet) {
			if(col.GetComponent<enemy1>()!=null)
				col.GetComponent<enemy1>().SendMessage ("Die");
			if(col.GetComponent<enemy2>()!=null)
				col.GetComponent<enemy2>().SendMessage ("Die");
			Destroy (this.gameObject);
		}
	}
}
