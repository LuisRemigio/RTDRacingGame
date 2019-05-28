using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : MonoBehaviour
{
    public LineRenderer thrusterLine;
    public Light thrusterLight;
    public float maxLength = 10;
    [Range(0, 0.1f)]
    public float flickerAmount = 0.1f;
    public float flickerSpeed = 60;
    public bool velocityBasedLength = false;
    public float velocityModifier = 10;

    float lightIntensity;
    float speed;
    float length;
    Color thrusterColor;
    Vector3 position;
    int numCapVertices = 2;

    // Start is called before the first frame update
    void Start()
    {
        numCapVertices = thrusterLine.numCapVertices;
        thrusterLine.SetPosition(1, Vector3.forward * length);
        thrusterColor = thrusterLine.material.GetColor("_TintColor");
        lightIntensity = thrusterLight.intensity;
        InvokeRepeating("Flicker", 0, 1 / flickerSpeed);

}

// Update is called once per frame
void Update()
    {
        thrusterLine.numCapVertices = thrusterLine.numCapVertices;
        length = maxLength;
        if (velocityBasedLength)
        {

            ComputeThrusterLength();
            length = Mathf.Clamp(speed, 0, maxLength);
        }
        
        if (speed <= 0 || Vector3.Dot(gameObject.transform.GetComponentInParent<Rigidbody>().velocity, gameObject.transform.forward) < 0)// == -gameObject.transform.forward)
        {
            //thrusterLine.numCapVertices = 0;
            thrusterLine.enabled = false;
        }

        else
        {
            thrusterLine.enabled = true;
            thrusterLine.numCapVertices = 15;
        }
        
    }

    void Flicker()
    {
        float noise = Random.Range(1 - flickerAmount, 1);
        thrusterLine.material.SetColor("_TintColor", thrusterColor * noise);
        thrusterLine.SetPosition(1, Vector3.forward * length * noise);
        thrusterLight.intensity = noise * (Mathf.Clamp(length, 0, 8));
    }

    void ComputeThrusterLength()
    {

        speed = velocityModifier * (transform.position - position).magnitude;
        numCapVertices = 0;
        position = transform.position;
    }

}
