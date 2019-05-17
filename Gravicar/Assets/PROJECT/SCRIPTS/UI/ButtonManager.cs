using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public GameObject pause;
    public bool paused = false;

    bool inMainMenu = false;

    void Update()
    {
        Pause();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            inMainMenu = true;
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inMainMenu == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void Loadlevel()
    {
        SceneManager.LoadScene("BrianAltScene");
        pause.SetActive(true);
    }

    public void Resume()
    {
        if(paused == true)
        {
            Time.timeScale = 1;
            pause.SetActive(false);
        }
    }

    public void LoadOption()
    {
        SceneManager.LoadScene("OptionScene");
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
