using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPauseMenu : MonoBehaviour {
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject skip;

    private bool nowGo;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && nowGo == true)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        if (nowGo == true)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Debug.Log("Load Menu");
        //Time.timeScale = 1f;
        //SceneManager.LoadScene ("Main");
        SceneManager.LoadScene("UITEST");
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void Yes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void No()
    {
        Destroy(skip);
        nowGo = true;
        Resume();
    }
}
