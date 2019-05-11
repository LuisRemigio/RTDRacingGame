using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTest : MonoBehaviour
{
    public float smooth = 1f;

    private Vector3 targetAngles;
    public float speed = 2f;
    public float maxRotation = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed), 0f, 0f);
        }
        
    }
}
