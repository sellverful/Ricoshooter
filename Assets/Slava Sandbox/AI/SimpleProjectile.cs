using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour {
    public float speed = 2f;
    private Rigidbody rig;
    public int damage= 1;
    public float destroyInSec = 10f;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyInSec);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rig.velocity = new Vector3(0,-speed,0);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("DealDamage",damage);
            Destroy(gameObject);
        }
    }
}
