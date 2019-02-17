using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Binds : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha3) && SceneManager.GetActiveScene().name != "BossSecond")
        {
            Load("BossSecond");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "FirstLevel")
        {
            Load("FirstLevel");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "FourthLevel_2")
        {
            Load("FourthLevel_2");
        }
    }
    void Load(string scene)
    {
        GameObject loaderObject = GameObject.Find("Death/LevelLoader");

        if (loaderObject != null)
        {
            LevelLoader loader = loaderObject.GetComponent<LevelLoader>();
            StartCoroutine(loader.LoadAsynchronously(scene));
        }
        else
        {
            SceneManager.LoadScene(scene);
        }

    }
}
