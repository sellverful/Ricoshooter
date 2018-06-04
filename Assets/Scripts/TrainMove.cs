using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour {
    [SerializeField]
    private float speed = 100;
    private bool isAllowedToTrigger;
    [SerializeField]
    private bool back;

    // Use this for initialization
    void Start () {
        isAllowedToTrigger = true;

        //Invoke("GetTheHellOuttaHere", 7.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (back)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        } else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        

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
