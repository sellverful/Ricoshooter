using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Titles : MonoBehaviour {
    //added
    public Transform skiptext;
    public int count;
	// Use this for initialization
	void Start () {
        skiptext.gameObject.SetActive(false);
        count = 0;
        StartCoroutine(GoToMainMenu());
	}
    //added
    private void Update()
    {
        if(count == 0 && Input.anyKey)
        {
            skiptext.gameObject.SetActive(true);
            count = +1;
        }
        if(count == 1 && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Skooped");
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(45f);
        SceneManager.LoadScene(0);
    }
}
