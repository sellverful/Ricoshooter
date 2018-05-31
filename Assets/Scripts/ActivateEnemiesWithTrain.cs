using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemiesWithTrain : MonoBehaviour {
    public Doors doors;
    public int enemiesAmount;
    public int count;
    public bool isHere;
    // Use this for initialization
    void Start () {
        enemiesAmount = transform.childCount;
        for (int i = 0; i < enemiesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Train")
        {
            for (int i = 0; i < enemiesAmount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (enemiesAmount == count)
        {
            if (doors != null)
                doors.doorsActive = true;
            Destroy(gameObject, 2);
        }
    }
}
