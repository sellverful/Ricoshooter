using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChange : MonoBehaviour {
    private GameObject tt1;
    private GameObject tt2;
	// Use this for initialization
	void Start () {
        tt1 = GameObject.FindGameObjectWithTag("TT1");
        tt2 = GameObject.FindGameObjectWithTag("TT2");
        tt2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            tt1.SetActive(false);
            tt2.SetActive(true);
        }
    }
}
