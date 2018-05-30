using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy2 : MonoBehaviour {
	[HideInInspector] public enum State{Patrol,Chase,Shoot,Die};
	public State currState = State.Chase;
	[HideInInspector] public enum ShootType{ShotGun,Burst,Sine};
	public ShootType currShootType = ShootType.Burst;
	public Transform player;
	NavMeshAgent agent;
	public float chaseDistance = 20f;
	private Vector3 shootDirection;
	public float errorMargin = 1f;
	private float shotCounter = 0f;
	public float timeBetweenShots;
	public EnemyBullet	 bullet;
	public SineBulletScript sineBullet;
	public float bulletSpeed;
	public Transform firePoint;

	public AudioClip shootSound;
	public AudioClip BurstShootSound;
	public AudioClip SineShootSound;
	public AudioClip dieSound;
	private Animator anim;
	private AudioSource audioS;
	// Use this for initialization

	void Start () {
		audioS = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		DoInstructionsAccordingToStatus ();
	}

	void DoInstructionsAccordingToStatus(){
		switch (currState)
		{
		case State.Patrol:
			PatrolRandomly ();
			break;
		case State.Chase:
			ChasePlayer ();
			anim.SetTrigger ("Chase");
			break;
		case State.Shoot:
			ShootPlayer ();
			anim.SetTrigger ("Shoot");
			break;
		case State.Die:
			agent.Stop ();
			break;
		default:
			print ("Incorrect intelligence level.");
			break;
		}
	}
	void PatrolRandomly(){
		if (Vector3.Distance (player.position, this.transform.position) < chaseDistance) {
			currState = State.Chase;
		}
	}
	void ChasePlayer(){
		transform.LookAt (player.position);
		agent.SetDestination (player.position);

		if (CanSeePlayer () && Vector3.Distance (player.position, transform.position) < chaseDistance) {
			currState = State.Shoot;
			agent.stoppingDistance = chaseDistance;
			CancelInvoke ();
		}
		shotCounter -= Time.deltaTime;
	}
	void ShootPlayer(){
		transform.LookAt (player.position);
		if (!CanSeePlayer () || Vector3.Distance (player.position, transform.position) > chaseDistance) {
			currState = State.Chase;
			InvokeRepeating ("DoDistanceShorter",0.2f,0.2f);
		}
			
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0) {
			shotCounter = timeBetweenShots;

			switch(currShootType){
			case ShootType.Burst:
				audioS.PlayOneShot (BurstShootSound);
				StartCoroutine (ShootBurst ());
				break;
			case ShootType.ShotGun:
				audioS.PlayOneShot (shootSound);
				ShotgunShoot();
				break;
			case ShootType.Sine:
				audioS.PlayOneShot (SineShootSound);
				InstansiatSineBullet ();
				break;
			}
		}
	}
	void DoDistanceShorter(){
		agent.stoppingDistance -= 5;
	}
	void ShotgunShoot(){
		
		for (int i = 0; i < 5; i++) {
			InstansiateShotGunBullet ((i-2)*5);
		}
	}


	IEnumerator ShootBurst(){
			for (int i = 0; i < 3; i++) {
				InstansiateBullet ();
				yield return new WaitForSeconds (0.1f);
			}
	}



	bool CanSeePlayer(){
		var ray = new Ray(transform.position, player.transform.position - transform.position);
		RaycastHit hit;
		Debug.DrawRay (transform.position, player.transform.position - transform.position);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.tag == "Player") {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
	void InstansiateShotGunBullet(int addRotation){
		shootDirection = player.position + Random.insideUnitSphere * errorMargin - transform.position;
		shootDirection = new Vector3 (shootDirection.x,0,shootDirection.z);
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
		EnemyBullet newBullet = Instantiate (bullet, firePoint.position, Quaternion.LookRotation(shootDirection)*Quaternion.Euler(0,addRotation,0)) as EnemyBullet;
		newBullet.speed = bulletSpeed;
	}
	void InstansiateBullet(){
		shootDirection = player.position + Random.insideUnitSphere * errorMargin - transform.position;
		shootDirection = new Vector3 (shootDirection.x,0,shootDirection.z);
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
			EnemyBullet newBullet = Instantiate (bullet, firePoint.position, Quaternion.LookRotation(shootDirection)) as EnemyBullet;
			newBullet.speed = bulletSpeed;
	}
	void InstansiatSineBullet(){
		SineBulletScript newBullet = Instantiate (sineBullet, transform.position, transform.rotation) as SineBulletScript;
		newBullet.speed = bulletSpeed;
	}
	void OnDestroy(){
		if(gameObject.GetComponentInParent<ActivateEnemies> ()!=null)
			gameObject.GetComponentInParent<ActivateEnemies> ().count++;
	}
	void Die(){
		anim.SetTrigger ("Die");
		currState = State.Die;
		GetComponent<BoxCollider> ().enabled = false;
		audioS.PlayOneShot (dieSound);
		Destroy (gameObject, 1.2f);
	}
}
