using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAKKA_DAKKA_DAKKA : MonoBehaviour {
    public GameObject bullet;
    [SerializeField]
    private GameObject whereTo;

	// Use this for initialization
	void Start () {
        
        InvokeRepeating("dakka_dakka", 0f, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void dakka_dakka()
    {
        Instantiate(bullet, whereTo.transform.position,whereTo.transform.rotation);
    }
}
