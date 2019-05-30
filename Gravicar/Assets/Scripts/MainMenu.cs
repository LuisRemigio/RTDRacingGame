using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject transitionPanel;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BeginningTransition());

    }
    public void PlayGame()
    {
        transitionPanel.SetActive(false);
        StartCoroutine(ScreenTransition());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        SceneManager.LoadScene("_OptionsMenu");
        StartCoroutine(ScreenTransition());

       // transitionPanel.SetActive(false);
    }
    public void OptionsBackbutton()
    {
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(ScreenTransition());

    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    IEnumerator ScreenTransition()
    {
        transitionPanel.SetActive(true);
        anim.Play("PlayTransitionPlayIN");
        yield return new WaitForSeconds(1);
        transitionPanel.SetActive(false);

    }

    IEnumerator BeginningTransition()
    {
        transitionPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        transitionPanel.SetActive(false);

    }
}
