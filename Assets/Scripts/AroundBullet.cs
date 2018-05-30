using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundBullet : MonoBehaviour {
    public SineBulletScript sineBullet;
    public Bullet bullet;
	// Use this for initialization
	void Start () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Wall")
            return;
        StartCoroutine(Splash());
    }
    IEnumerator Splash()
    {
        yield return new WaitForSeconds(0.02f);
        Vector3 way = new Vector3(transform.forward.x, 0, transform.forward.z);
        for (int i = 0; i < 24; i++)
        {
            if (i < 8 || i > 16)
            {
                if (i % 2 != 0)
                {
                    Instantiate(bullet, transform.position, Quaternion.LookRotation(way) * Quaternion.Euler(0, i * 15, 0));
                }
                else if (i % 2 == 0)
                {
                    Instantiate(sineBullet, transform.position, Quaternion.LookRotation(way) * Quaternion.Euler(0, i * 15, 0));
                }
            }
        }
        Destroy(gameObject);
    }
}
