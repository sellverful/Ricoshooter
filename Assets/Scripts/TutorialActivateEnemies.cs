using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivateEnemies : MonoBehaviour {
    public Doors doors;
    public int enemiesAmount;
    public int count;
    public bool isHere;
    private GameObject wallsToDestroy;
    // Use this for initialization
    void Start()
    {
        enemiesAmount = transform.childCount;
        for (int i = 0; i < enemiesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        //wallsToDestroy = GameObject.FindGameObjectWithTag("Dissapear");
        //wallsToDestroy.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {
            for (int i = 0; i < enemiesAmount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            //wallsToDestroy.SetActive(false);
        }
    }
    void Update()
    {
        if (enemiesAmount == count)
        {
            if (doors != null)
                doors.doorsActive = true;
            Destroy(gameObject, 2);
        }
    }

}
