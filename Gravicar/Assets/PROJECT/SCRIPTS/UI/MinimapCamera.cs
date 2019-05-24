using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    GameObject player;
    public int high;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Vehicle");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + high, player.transform.position.z);
        transform.rotation = Quaternion.Euler(90,-90, player.transform.rotation.y - 90);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -player.transform.eulerAngles.y);
        //transform.up = player.transform.forward;
        //transform.RotateAround(transform.right, -90);
        //transform.rotation = new Quaternion(, player.transform.rotation.y, player.transform.rotation.z, transform.rotation.w);

    }
}
