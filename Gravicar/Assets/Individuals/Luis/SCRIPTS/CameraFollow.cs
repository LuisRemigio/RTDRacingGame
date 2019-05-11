using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        if (car == null)
            car = GameObject.Find("PlayerCar");
        if (car == null)
            Debug.Log("Player vehicle not assigned to camera.");
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(car.transform.position.x, car.transform.position.y + 2.0f, car.)
    }
}
