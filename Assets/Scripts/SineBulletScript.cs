using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBulletScript : MonoBehaviour {
    public bool playersBullet = false;
	public float speed = 5.0f;
	public int damage = 1;
	public float frequency = 1f;  // Speed of sine movement
	public float magnitude = 3f;   // Size of sine movement
	private Vector3 axis;
	private float startTime;
	private Vector3 pos;

	private PlayerController player;
	void Start () {
		pos = transform.position;
		//DestroyObject(gameObject, 5.0f);
		axis = transform.right;  // May or may not be the axis you want
		startTime = Time.time;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		Destroy (this.gameObject, 5f);
	}

	void Update () {
		pos += transform.forward * Time.deltaTime * speed;
		transform.position = pos + axis * Mathf.Sin ((Time.time-startTime) * frequency) * magnitude;
		Reflect ();
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && player.deflect == false)
        {
            player.SendMessage("DealDamage", 1);
            Destroy(this.gameObject);
        }
        else if (col.tag == "Defence" && playersBullet)
        {
            playersBullet = false;
            if (col.GetComponent<ShieldDeflect>() != null)
                col.GetComponent<ShieldDeflect>().SendMessage("ReceiveDamage");
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (col.tag == "Enemy" && playersBullet)
        {
            if (col.GetComponent<enemy1>() != null)
                col.GetComponent<enemy1>().SendMessage("Die");
            if (col.GetComponent<enemy2>() != null)
                col.GetComponent<enemy2>().SendMessage("Die");
            Destroy(this.gameObject);
        }
    }
    void Reflect(){
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit, 1f)){
			if((hit.collider.tag == "Player" && player.deflect) || hit.collider.tag == "Wall" || (hit.collider.tag == "Enemy" && !playersBullet)){
				Vector3 reflect = Vector3.Reflect (ray.direction, hit.normal);
				float rot = 90 - Mathf.Atan2 (reflect.z, reflect.x) * Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3 (0, rot, 0);
			}


		}
	}
}
