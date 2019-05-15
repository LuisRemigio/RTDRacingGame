using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    public Text text;
    private double Speed;
    private Vector3 startingPosition, speedvec;


    void Start()
    {
        startingPosition = transform.position;
    }

    
    void Update()
    {
        speedvec = ((transform.position - startingPosition) / Time.deltaTime);
        Speed = (int)(speedvec.magnitude) * 15; // 3.6 is the constant to convert a value from m/s to km/h, because i think that the speed wich is being calculated here is coming in m/s, if you want it in mph, you should use ~2,2374 instead of 3.6 (assuming that 1 mph = 1.609 kmh)

        startingPosition = transform.position;
        text.text = Speed + "km/h";  // or mph
    }
}
