using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getsuga : MonoBehaviour {

    private Rigidbody rig;

    public float speed = 5f;
    public float secondsToLive = 5f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject,secondsToLive);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector3.forward * Time.deltaTime*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().SendMessage("DealDamage",1);
        }
    }
}
