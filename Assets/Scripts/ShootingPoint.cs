using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoint : MonoBehaviour {
	[HideInInspector] public enum ShootType{ShotGun,Burst,Sine};
	public ShootType currShootType = ShootType.Burst;
	private Vector3 shootDirection;
	public float errorMargin = 1f;
	private float shotCounter = 0f;
	public float timeBetweenShots;
	public EnemyBullet bullet;
	public SineBulletScript sineBullet;
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		Shoot ();
	}

	void Shoot(){
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0) {
			shotCounter = timeBetweenShots;
			switch(currShootType){
			case ShootType.Sine:
				InstansiatSineBullet ();
				break;
			case ShootType.Burst:
				StartCoroutine (ShootBurst ());
				break;
			case ShootType.ShotGun:
				ShotgunShoot();
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
			yield return new WaitForSeconds (0.1f);
		}
	}

	void InstansiatSineBullet(){
		SineBulletScript newBullet = Instantiate (sineBullet, transform.position, transform.rotation) as SineBulletScript;
		newBullet.speed = bulletSpeed;
	}
	void InstansiateShotGunBullet(int addRotation){
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
		EnemyBullet newBullet = Instantiate (bullet, transform.position, transform.rotation*Quaternion.Euler(0,addRotation,0)) as EnemyBullet;
		newBullet.speed = bulletSpeed;
	}
	void InstansiateBullet(){
		Debug.DrawLine(this.transform.position,shootDirection,Color.red);
		EnemyBullet newBullet = Instantiate (bullet, transform.position,transform.rotation) as EnemyBullet;
		newBullet.speed = bulletSpeed;
	}
}
