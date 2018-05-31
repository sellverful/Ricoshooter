using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour {
    private float speed;
    private bool isAllowedToTrigger;
    public ActivateEnemiesWithTrain aewt;
	// Use this for initialization
	void Start () {
        speed = 40f;
        isAllowedToTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if(aewt.enemiesAmount == 0)
        {
            isAllowedToTrigger = false;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Collider");
        if(other.gameObject.tag == "Stopper" && isAllowedToTrigger == true)
        {
            //Debug.Log("Stopper");
            slowDown();
        }
        else if(other.gameObject.tag == "Stopper" && isAllowedToTrigger == false)
        {
            Debug.Log("Ехай Нахуй");
            speed = 40f;
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
}
