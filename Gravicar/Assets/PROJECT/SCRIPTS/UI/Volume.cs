using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<Slider>().value = 1;
    }

    public void changeVolume()
    {
        AudioListener.volume = this.gameObject.GetComponent<Slider>().value;
    }
}
