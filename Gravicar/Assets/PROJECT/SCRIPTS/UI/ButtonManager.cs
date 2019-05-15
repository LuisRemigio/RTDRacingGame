using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    
    void Start()
    {
        
    }


    void Update()
    {
        Pause();
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
    }

    public void LoadMeun()
    {
        SceneManager.LoadScene("");
        //Load main meun
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        //Load defferent level scenes
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        //Restart this level
    }

    public void Quit()
    {
        Application.Quit();
        //Quit game
    }

}
