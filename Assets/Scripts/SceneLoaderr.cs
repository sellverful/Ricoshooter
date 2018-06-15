using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoaderr : MonoBehaviour
{

    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}