using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCarIcon : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Vehicle");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x - 40, player.transform.position.y + 20, player.transform.position.z + 19);
        //transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
        //transform.Rotate(transform.up, 90);
    }
}
