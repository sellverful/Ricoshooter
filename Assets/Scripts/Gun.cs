using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [HideInInspector] public enum ShootType { Gun, Laser, SineShotgun, Around, DoubleGun };
    public ShootType currShootType = ShootType.Gun;
    public bool isFiring;
    public float ammoLazer = 2f;

    public SineBulletScript sineBullet;
    public Bullet aroundBullet;
    public Bullet bullet;
    public float bulletSpeed;
    public float boostUpDuration = 10f;
    public float timeBetweenShots;
    public float timeBetweenShotsAround = 1f;
    private float shotCounter = 0f;

    public GameObject laser;
    public Transform firePoint;

    public AudioClip shurikenSound;



    // Use this for initialization
    void Start()
    {

    }

    void ButtonPressed()
    {
        if (Input.GetButton("SecondGun"))
        {
            currShootType = ShootType.SineShotgun;
        }
    }
    void ShootingTypes()
    {

        switch (currShootType)
        {
            case ShootType.Gun:
                ShootWithGun();
                break;
            case ShootType.SineShotgun:
                SineShotgunShoot();
                break;
            case ShootType.Around:
                AroundShoot();
                break;
            case ShootType.DoubleGun:
                DoubleShoot();
                break;
        }


    }
    // Update is called once per frame
    void Update()
    {
        //ButtonPressed ();
        ShootingTypes();
    }
    void ShootWithLaser()
    {
        if (isFiring)
        {
            laser.SetActive(true);
        }
        else
        {
            laser.SetActive(false);
        }
    }
    void ShootWithGun()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(shurikenSound);
                Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                newBullet.speed = bulletSpeed;
                shotCounter = timeBetweenShots;
            }


        }
        else
        {
            shotCounter = 0;
        }

    }
    void ChangeShootType(ShootType shoot)
    {
        currShootType = shoot;
        StartCoroutine(ChangeShootTypeBack());
    }
    IEnumerator ChangeShootTypeBack()
    {
        yield return new WaitForSeconds(boostUpDuration);
        currShootType = ShootType.Gun;
    }
    void SineShotgunShoot()
    {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {

            if (shotCounter <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    InstansiateSineShotGunBullet((i - 2) * 5);
                }
                shotCounter = timeBetweenShots;
            }


        }

    }
    void DoubleShoot()
    {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {

            if (shotCounter <= 0)
            {
                InstansiateDoubleBullet();
                shotCounter = timeBetweenShots;
            }


        }
    }
    void AroundShoot()
    {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {

            if (shotCounter <= 0)
            {
                InstansiateAroundBullet();
                shotCounter = timeBetweenShotsAround;
            }


        }
    }
    void InstansiateSineShotGunBullet(int addRotation)
    {
        Vector3 way = new Vector3(transform.forward.x, 0, transform.forward.z);
        SineBulletScript newBullet = Instantiate(sineBullet, firePoint.position, Quaternion.LookRotation(way) * Quaternion.Euler(0, addRotation, 0)) as SineBulletScript;
        newBullet.speed = 25;
    }
    void InstansiateAroundBullet()
    {
        Bullet newBullet = Instantiate(aroundBullet, firePoint.position, firePoint.rotation) as Bullet;
        newBullet.speed = bulletSpeed;
    }
    void InstansiateDoubleBullet()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i % 2 == 0)
            {
                StartCoroutine(DoubleBurst(15));
            }
            else
            {
                StartCoroutine(DoubleBurst(345));
            }

        }
    }
    IEnumerator DoubleBurst(float rotation)
    {
        Vector3 way = new Vector3(transform.forward.x, 0, transform.forward.z);
        for (int i = 0; i < 3; i++)
        {
            Bullet newBullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(way) * Quaternion.Euler(0, rotation, 0)) as Bullet;
            newBullet.speed = bulletSpeed;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
