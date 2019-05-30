using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Speedometer : MonoBehaviour
{
    public Image myImage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            //Reduce fill amount over 30 seconds
            myImage.fillAmount += 1.0f / 4 * Time.deltaTime;
        }
        else
        {
            myImage.fillAmount -= 1.0f / 1 * Time.deltaTime;

        }
    }
}
