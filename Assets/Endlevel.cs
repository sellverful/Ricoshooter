using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour {
    public GameObject RestartUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        RestartUI.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
    }
}
