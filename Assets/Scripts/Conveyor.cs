using UnityEngine;
using System.Collections;

public class Conveyor : MonoBehaviour {

    public float speed = 2.0f;
	private Rigidbody rig;
	void Start(){
		rig = GetComponent<Rigidbody> ();
	}

    void FixedUpdate()
    {
		rig.position -= transform.right * speed * Time.deltaTime;
		rig.MovePosition (rig.position + transform.right * speed * Time.deltaTime);

    }

}