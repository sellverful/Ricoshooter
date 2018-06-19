using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
	public Transform player;



	[HideInInspector] public enum ShootType{Burst=0,ShotGun=1,Sine=2,Mega=3,BulletHell=4,Vulnarable=5};
	public ShootType currShootType = ShootType.Burst;
	private Vector3 shootDirection;
	public float errorMargin = 1f;
	private float shotCounter = 0f;
	public float timeBetweenShots;
	public EnemyBullet bullet;
	public SineBulletScript sineBullet;
	public float bulletSpeed;
	public Transform[] firePoint;
	public Transform[] additionalFirePoints;
	public int currentFirePoint = 0;
	public float rotationSpeed = 500;
	public Transform shields;
	public float vulnarableSeconds = 3;
	public float bulletHellTime = 6;
	public Animator anim;
	public Transform explosion;
	public Transform spinner;
	private int shieldsAmount;
	private Quaternion rotation;
	private bool once = false;
	private ShootType savedShootType;
	private int currGun = 0;
	public AudioClip ShotGun;
	public AudioClip BurstGun;
	public AudioClip SineGun;
	public AudioClip VulnerableHit;
	public bool dead = false;
	private bool deadOnce = false;
	private AudioSource au;
	// Use this for initialization
	void Start () {
		au = GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Physics.IgnoreCollision (GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag ("Plane").GetComponent<MeshCollider>());
		shieldsAmount = shields.transform.childCount;
		anim = GameObject.Find ("Spinner").GetComponent<Animator> ();
		//InvokeRepeating ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (player.position);
		//rotation = Quaternion.LookRotation (player.position - transform.position);
		//rotation *= Quaternion.Euler (0, 90, 0);
		//transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 1);
		Debug.DrawLine (transform.position,player.position);
		LookAt ();
		if (!dead) {
			Shoot ();
			CheckShields ();
		}
		if (dead && !deadOnce) {
			currShootType = ShootType.Vulnarable;
			deadOnce = true;
			StartCoroutine (DeactivateSpinner());
		}
	}


	void LookAt(){
		switch(currShootType){
		case ShootType.Sine:
			rotation = Quaternion.LookRotation (player.position - transform.position);
			rotation *= Quaternion.Euler (0, 123, 0);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.1f);
			break;
		case ShootType.Burst:
			rotation = Quaternion.LookRotation (player.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.1f);
			break;
		case ShootType.ShotGun:
			rotation = Quaternion.LookRotation (player.position - transform.position);
			rotation *= Quaternion.Euler (0, -123, 0);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.1f);
			break;
		case ShootType.Mega:
			rotation = Quaternion.LookRotation (player.position - transform.position);
			RotateAsHell ();
			break;
		case ShootType.BulletHell:
			StartBulletHell ();
			break;
		case ShootType.Vulnarable:
			GetComponent<NavMeshAgent> ().enabled = false;
			GetComponent<Wandering> ().enabled = false;
			break;
		}

	}
	void Shoot(){
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0) {
			shotCounter = timeBetweenShots;
			switch(currShootType){
			case ShootType.Sine:
				currentFirePoint = 2;
				au.PlayOneShot (SineGun);
				InstansiatSineBullet ();
				break;
			case ShootType.Burst:
				currentFirePoint = 0;

				StartCoroutine (ShootBurst ());
				break;
			case ShootType.ShotGun:
				au.PlayOneShot (ShotGun);
				currentFirePoint = 1;
				ShotgunShoot();
				break;
			case ShootType.Mega:
				for (int i = 0; i < additionalFirePoints.Length; i++) {
					additionalFirePoints [i].gameObject.SetActive (true);
				}
				currentFirePoint = 0;
				if(!au.isPlaying)
					au.Play ();
				StartCoroutine (ShootBurst ());
				break;
			case ShootType.Vulnarable:
				au.Stop ();
				break;
			default:
				currentFirePoint = 0;
				StartCoroutine (ShootBurst ());
				break;
			}
		}
	}
	void ShotgunShoot(){
		for (int i = 0; i < 5; i++) {
			InstansiateShotGunBullet ((i-2)*5);
		}
	}
	IEnumerator ShootBurst(){
		for (int i = 0; i < 3; i++) {
			InstansiateBullet ();
			if(currShootType == ShootType.Burst)
				au.PlayOneShot (BurstGun);
			yield return new WaitForSeconds (0.1f);

		}
	}
	void InstansiateShotGunBullet(int addRotation){
		shootDirection = player.position + Random.insideUnitSphere * errorMargin - transform.position;
		shootDirection = new Vector3 (shootDirection.x,0,shootDirection.z);
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
		EnemyBullet newBullet = Instantiate (bullet, firePoint[currentFirePoint].position, Quaternion.LookRotation(shootDirection)*Quaternion.Euler(0,addRotation,0)) as EnemyBullet;
		newBullet.speed = bulletSpeed;
	}
	void InstansiateBullet(){
		shootDirection = player.position + Random.insideUnitSphere * errorMargin - transform.position;
		shootDirection = new Vector3 (shootDirection.x,0,shootDirection.z);
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
		EnemyBullet newBullet = Instantiate (bullet, firePoint[currentFirePoint].position, Quaternion.LookRotation(shootDirection)) as EnemyBullet;
		newBullet.speed = bulletSpeed;
	}
	void InstansiatSineBullet(){
		shootDirection = player.position + Random.insideUnitSphere * errorMargin - transform.position;
		shootDirection = new Vector3 (shootDirection.x,0,shootDirection.z);
		SineBulletScript newBullet = Instantiate (sineBullet, firePoint[currentFirePoint].position, Quaternion.LookRotation(shootDirection)) as SineBulletScript;
	}
	void StartBulletHell(){
		currShootType = ShootType.BulletHell;
		RotateAsHell ();
		if (once == false) {
			au.Play (); // play sound MEGA
			StartCoroutine (EnableFlyingObject());
			StartCoroutine (DisableFlyingObject());
            GameObject.Find("HeartSpawner").SendMessage("spawn");
			once = true;
		}

	}
	void RotateAsHell(){
		transform.Rotate (Vector3.up, rotationSpeed * Time.deltaTime);
	}
	IEnumerator EnableFlyingObject(){
		anim.SetTrigger ("BulletHellOn");
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<Wandering> ().enabled = false;

		yield return new WaitForSeconds (1f);
		GetComponent<FlyingObject> ().enabled = true;
		Rigidbody rig = GetComponent<Rigidbody> ();
		rig.isKinematic = false;
		rig.velocity = transform.forward * GetComponent<FlyingObject> ().speed;
		for (int i = 0; i < additionalFirePoints.Length; i++) {
			additionalFirePoints [i].gameObject.SetActive (true);
		}
	}
	IEnumerator DisableFlyingObject(){
		
		yield return new WaitForSeconds (bulletHellTime);
		anim.SetTrigger ("BulletHellOff");
		GetComponent<FlyingObject> ().enabled = false;
		GetComponent<NavMeshAgent> ().enabled = true;
		GetComponent<Wandering> ().enabled = true;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		for (int i = 0; i < additionalFirePoints.Length; i++) {
			additionalFirePoints [i].gameObject.SetActive (false);
		}
		ShieldsUp ();
		currShootType = (ShootType)currGun;
		once = false;
		au.Stop ();
	}

	//Shields
	void ShieldsUp(){
		for (int i = 0; i < shieldsAmount; i++) {
			shields.transform.GetChild (i).gameObject.SetActive (true);
		}
	}
	void CheckShields(){
		if (currShootType!=ShootType.Vulnarable && currShootType!=ShootType.BulletHell) {
			if(!shields.gameObject.activeSelf) shields.gameObject.SetActive (true);
			int checkingshields = 0;
			for (int i = 0; i < shieldsAmount; i++) {
				if (!shields.transform.GetChild (i).gameObject.activeSelf) {
					checkingshields++;
				}
			}
			if (checkingshields == shieldsAmount) {
				ActivateVulnerablePoint ();
			}
		}
	}
	void ActivateVulnerablePoint(){
		currShootType = ShootType.Vulnarable;
		if (currGun == 3) {
			DestroyAllBullets ();
			for (int i = 0; i < additionalFirePoints.Length; i++) {
				additionalFirePoints [i].gameObject.SetActive (false);
			}
		}
		if (currGun < 3) {
			firePoint [currentFirePoint].parent.parent.GetComponent<VulnerablePoint> ().enabled = true;
		}	
		StartCoroutine (DeactivateVulnerablePoint());
	}
	void DestroyAllBullets(){
		GameObject[] these = GameObject.FindGameObjectsWithTag ("Bullet");
		foreach (GameObject obj in these) {
			Destroy (obj);
		}
	}
	IEnumerator DeactivateVulnerablePoint(){
		yield return new WaitForSeconds (vulnarableSeconds);
		if (firePoint [currentFirePoint].parent.parent.GetComponent<VulnerablePoint> ().destroyed) {
			if (currGun < 3) {
				currGun++;
			}
			else {
				StartCoroutine(DeactivateSpinner ());
				dead = true;
                StartCoroutine(EndGame());
			}
		}
		firePoint [currentFirePoint].parent.parent.GetComponent<VulnerablePoint> ().enabled = false;
		if (!dead) {
			ShieldsUp ();
			StartBulletHell ();
		}

	}
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
	IEnumerator DeactivateSpinner(){
		explosion.gameObject.SetActive (true);
		yield return new WaitForSeconds (1f);
		spinner.gameObject.SetActive (false);
		yield return new WaitForSeconds (2f);
		explosion.gameObject.SetActive (false);
	}
}
