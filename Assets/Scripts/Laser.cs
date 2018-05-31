using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	private LineRenderer lr;
	public float laserDistance = 30;
	public AudioClip laserSound;
	private Transform player;
	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void OnEnable(){
		GetComponent<AudioSource> ().Play (); //(laserSound);
	}
	// Update is called once per frame
	void Update () {
		if (GetComponentInParent<Gun> ().isFiring) {
			lr.positionCount = 3;
			lr.SetPosition (0, transform.position);
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);
			if (Physics.Raycast (transform.position, transform.forward, out hit, laserDistance)) {
				lr.useWorldSpace = true;
				if (hit.collider) {
					if (hit.collider.tag == "Enemy")
						KillEnemy (hit.collider.gameObject);
						lr.SetPosition (1, hit.point);
					if (hit.collider.tag == "Wall" || hit.collider.tag == "Box") {
						Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
						if (Physics.Raycast (hit.point, reflect, out hit, laserDistance)) {
							if (hit.collider.tag == "Enemy")
								KillEnemy (hit.collider.gameObject);
							lr.positionCount = 3;
							lr.SetPosition (2, hit.point);
						} else {
							Ray distanceRay = new Ray (hit.point, reflect);
							lr.positionCount = 3;
							lr.SetPosition (2, distanceRay.GetPoint (laserDistance));
						}
					} else {
						lr.positionCount = 2;
					}
				}
			} else {
				lr.positionCount = 2;
				Ray distanceRay = new Ray (transform.position, transform.forward);
				lr.SetPosition (0, transform.root.position);
				lr.SetPosition (1, distanceRay.GetPoint(laserDistance));
			}
		} else {
			lr.positionCount = 0;
		}


	}
	void KillEnemy(GameObject enemy){
		if(enemy.GetComponent<enemy1>()!=null)
			enemy.GetComponent<enemy1>().SendMessage ("Die");
		if(enemy.GetComponent<enemy2>()!=null)
			enemy.GetComponent<enemy2>().SendMessage ("Die");
	}

}
