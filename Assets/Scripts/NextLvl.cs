using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour {

    public GameObject endScore;
    private GameObject score;
    public void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score");
        endScore.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Entering new Level");
            other.GetComponent<PlayerController>().levelEnd = true;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        score.SetActive(false);
        endScore.SetActive(true);
        //yield return new WaitForSeconds(0.2f);
        //Time.timeScale = 0f;
    }
    ////Use this for initialization
    //void OnTriggerEnter(Collider col){
    //	if(col.tag =="Player")
    //		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
