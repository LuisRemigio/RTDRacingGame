using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathContainer : MonoBehaviour
{
    [SerializeField] List<GameObject> checkpointLists;
    [SerializeField] List<GameObject> AIPaths;
    [SerializeField] List<GameObject> vehiclePrefabs;
    Transform[] spawns;
    [SerializeField] int playerVehicleIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int listIndex = scene.buildIndex - 5;
        if (GameObject.FindGameObjectsWithTag("Track") != null)
        {
            spawns = GameObject.Find("Spawns").GetComponentsInChildren<Transform>();
            for (int i = 0; i < vehiclePrefabs.Count; i++)
            {
                GameObject v = Instantiate(vehiclePrefabs[i]);
                v.transform.SetPositionAndRotation(spawns[i + 1].position, spawns[i + 1].rotation);
                v.GetComponent<Vehicle>().setCheckpointList(Instantiate(checkpointLists[listIndex]));
                if (i == playerVehicleIndex)
                {
                    v.GetComponent<Vehicle>().setPlayer(true);
                    v.tag = "Player";
                }
                else
                {
                    v.GetComponent<Vehicle>().setAIPaths(AIPaths.ToArray());
                }
            }
        }
    }

    public void setPlayerVehicle(int index)
    {
        playerVehicleIndex = index;
    }
}
