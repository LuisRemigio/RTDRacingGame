using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    GameObject player;

    public bool follow;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Vehicle");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (follow == false)
        {
            return;
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 500, player.transform.position.z)
        }
    }
}
