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
        speed = 25f;
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
            if (doors != null)
                doors.doorsActive = true;
        }
        if (enemiesAmount == count)
        {
            Debug.Log("jkh");
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
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    IEnumerator MoveReleaseMove()
    {
        StartCoroutine(Release());
        StartCoroutine(Wait());
        yield return Release();
        stop = false;
    }
    IEnumerator Release()
    {
        List<GameObject> list = new List<GameObject>();
        //Debug.Log(enemiesAmount);
        for (int i = 0; i < enemiesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            //Debug.Log(i);
            list.Add(transform.GetChild(i).gameObject);
            yield return new WaitForSeconds(0.1f);
        }
        foreach(GameObject obj in list)
        {
            obj.transform.parent = null;
        }
            yield return new WaitForSeconds(2);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
