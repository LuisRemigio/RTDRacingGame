using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ScreenTransition()
    {
        anim.Play("PlayTransitionPlayIN");
        Debug.Log("TRANSITION");
    }

}
