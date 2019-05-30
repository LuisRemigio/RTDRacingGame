using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float Skyboxspeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * Skyboxspeed);
    }
}
