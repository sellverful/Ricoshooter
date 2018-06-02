using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railroad : MonoBehaviour {
    private bool stop;
    private float speed;
    public Doors doors;
    public int enemiesAmount;
    public int count;
    public bool isHere;
    // Use this for initialization
    void Start () {
        speed = 50f;
        enemiesAmount = transform.childCount;
        for (int i = 0; i < enemiesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!stop){
            Move();
        }
        if (enemiesAmount == count)
        {
            if (doors != null)
                doors.doorsActive = true;
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Stopper")
        {
            Debug.Log("Stop!");
            Destroy(other);
            stop = true;
            StartCoroutine(MoveReleaseMove());
        }
        if(other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
            Destroy(other);
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
    IEnumerator MoveReleaseMove()
    {
        StartCoroutine(Release());
        StartCoroutine(Wait());
        stop = false;
        yield return null;        
    }
    IEnumerator Release()
    {
        //Debug.Log(enemiesAmount);
        for (int i = 0; i < enemiesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).parent = null;
            //Debug.Log(i);
            StartCoroutine(Wait());
            //yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
