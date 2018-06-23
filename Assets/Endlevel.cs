using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour {
    public GameObject endScore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerController>().levelEnd = true;
            StartCoroutine(Restart());
        }
        
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        endScore.SetActive(true);
        //yield return new WaitForSeconds(0.1f);
       // Time.timeScale = 0f;
    }
}
