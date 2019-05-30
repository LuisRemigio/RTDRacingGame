using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public GameObject pause;
    public bool paused = false;
    //public Animator menuAnim;
    //public Animator optionAnim;
    //public GameObject option;
    //public GameObject menu;

    bool inMainMenu = false;
    Vector3 currentPosition;
    Vector3 optionCurrentPosition;

    ArtificialGravity artificialGravity;

    void Awake()
    {
        //if(FindObjectOfType<ButtonManager>() != this)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        GetPosition();
        GetOptionPosition();
        if (FindObjectOfType<ArtificialGravity>())
        {
            artificialGravity = FindObjectOfType<ArtificialGravity>().GetComponent<ArtificialGravity>();
        }
    }

    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            inMainMenu = true;
        }
        Pause();
    }

    void GetPosition()
    {
        currentPosition = new Vector3(transform.position.x, transform.position.y + 19, transform.position.z);
    }

    void GetOptionPosition()
    {
        //optionCurrentPosition = new Vector3(option.transform.position.x, option.transform.position.y + 2, option.transform.position.z);
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inMainMenu == false)
        {
            paused = true;
            pause.SetActive(true);
            if(artificialGravity == null)
            {
                if (FindObjectOfType<ArtificialGravity>())
                {
                    artificialGravity = FindObjectOfType<ArtificialGravity>().GetComponent<ArtificialGravity>();
                }
            }
            artificialGravity.enabled = false;
            Time.timeScale = 0;
        }
    }

    public void Loadlevel()
    {
        SceneManager.LoadScene("VehicleSelect");
        Time.timeScale = 1;
        //pause.SetActive(true);
    }

    public void Resume()
    {
        if(paused == true)
        {
            
        }
        pause.SetActive(false);
        Time.timeScale = 1;
        artificialGravity.enabled = true;
        paused = false;
    }

    public void LoadOption()
    {
        SceneManager.LoadScene("OptionScene");
        //menuAnim.SetBool("clicked", true);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("PlanetOne");
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleMenu");
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

    public void DisActive()
    {
        transform.position = currentPosition;
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(false);
    }

    public void SetOptionActive()
    {
        //option.SetActive(true);
    }

    public void SetOptionDisactive()
    {
        //option.transform.position = optionCurrentPosition;
        //option.SetActive(false);
    }

    public void BackToMainMenu()
    {
        //optionAnim.SetBool("clicked", true);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMenuActive()
    {
        //menu.SetActive(true);
    }

    public void AnimationBool()
    {
        //menuAnim.SetBool("clicked", false);
        //optionAnim.SetBool("clicked", false);
    }

}
