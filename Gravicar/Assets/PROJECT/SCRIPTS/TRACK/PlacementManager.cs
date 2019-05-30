using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] List<Vehicle> vehicles = new List<Vehicle>();
    [SerializeField] GameObject placeHUD;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject vehicle in GameObject.FindGameObjectsWithTag("Vehicle"))
        {
            if (vehicle.GetComponent<Vehicle>() != null)
            {
                vehicles.Add(vehicle.GetComponent<Vehicle>());
            }
        }
        //if (vehicle.GetComponent<Vehicle>() != null)
            vehicles.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Vehicle>());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vehicles.Count; i++)
        {
            vehicles[i].setPlacement(1);
            float distance = vehicles[i].calculateDistance();
            int checks = vehicles[i].getCheckpointsLeft();
            for (int j = 0; j < vehicles.Count; j++)
            {
                if (j != i && (distance > vehicles[j].calculateDistance() || checks > vehicles[j].getCheckpointsLeft()))
                {
                    vehicles[i].setPlacement(vehicles[i].getPlacement() + 1);
                }
            }
            if (vehicles[i].tag == "Player" && placeHUD != null)
            {
                placeHUD.GetComponent<Text>().text = "" + vehicles[i].getPlacement();
            }
        }
    }
}
