using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour {
    private float speed;
    private bool isAllowedToTrigger;

	// Use this for initialization
	void Start () {
        speed = 40f;
        isAllowedToTrigger = true;

        //Invoke("GetTheHellOuttaHere", 7.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

	}
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Collider");
        if(other.gameObject.tag == "Stopper")
        {
            //Debug.Log("Stopper");
            slowDown();
        }
        
    }

    void slowDown()
   {
        while(speed > 1.0f)
        {
            speed -= 0.99f * Time.deltaTime;
            if (speed < 5.0f)
            {
                speed = 0f;
            }
        }    
   }
   /* void GetTheHellOuttaHere()
    {
        isAllowedToTrigger = false;
    }*/
}
