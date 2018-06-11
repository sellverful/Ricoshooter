using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAKKA_DAKKA_DAKKA : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	void Start () {
        
        InvokeRepeating("dakka_dakka", 0f, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void dakka_dakka()
    {
        Instantiate(bullet, new Vector3(32.74f, 0.63f, -159.04f), Quaternion.AngleAxis(90, Vector3.up));
        Instantiate(bullet, new Vector3(32.74f, 0.63f, -163.08f), Quaternion.AngleAxis(90, Vector3.up));
    }
}
