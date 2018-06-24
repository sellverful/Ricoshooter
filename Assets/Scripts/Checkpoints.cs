using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoints : MonoBehaviour {

    public static Checkpoints _instance;
    public int currentCheckpoint;
    public Vector3 checkpoint;
    public string currentScene;
    //protected only allows initialization from inside this class or it's children
    protected Checkpoints()
    {
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            currentScene = SceneManager.GetActiveScene().name;
  //          Debug.Log(currentScene);
            _instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    private void OnLevelWasLoaded()
    {
        if (checkpoint != Vector3.zero && currentScene==SceneManager.GetActiveScene().name)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint;
            Debug.Log("Here");
        }
        if(currentScene != SceneManager.GetActiveScene().name)
        {
            checkpoint = Vector3.zero;
        }
        Debug.Log("Here1");
        Debug.Log(checkpoint);
        StartCoroutine(showMe());
    }
    IEnumerator showMe()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(checkpoint);
    }
    public void ChangeCheckPoint(Transform checkpointTransform)
    {
            checkpoint = checkpointTransform.transform.position;
            currentCheckpoint++;
            Destroy(checkpointTransform.gameObject);
    }
}
