using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Titles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GoToMainMenu());
	}
	
	IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(45f);
        SceneManager.LoadScene(0);
    }
}
