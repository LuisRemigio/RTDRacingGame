using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleRotator : MonoBehaviour

{

    float timer = 0;

    Transform myT;

    // Start is called before the first frame update
    void Start()
    {
        myT = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myT.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
