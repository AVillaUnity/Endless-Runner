using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxBuildingLength = 7.5f;
    public float gapSpace = 3.0f;
    public int maxBuildingsSpawned = 16;
    public GameObject[] objectToSpawn;

    private int lastBuildingSpawned = -1;

    protected float spawnLocation = 0.0f;
    protected List<GameObject> spawnedBuildings;
    protected GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnedBuildings = new List<GameObject>();
        for(int i = 0; i < maxBuildingsSpawned; i++)
        {
            SpawnBuilding();
        }    
    }

    public virtual void Update()
    {
        if(player.transform.position.z - 10.0f > spawnLocation - (maxBuildingsSpawned * maxBuildingLength))
        {
            SpawnBuilding();
            DeleteBuilding();
        }
        
    }

    public virtual void SpawnBuilding()
    {
        int index = GetRandomBuilding(objectToSpawn.Length);

        GameObject building = Instantiate(objectToSpawn[index], transform.forward * spawnLocation, Quaternion.identity, transform);

        spawnLocation += building.GetComponent<Building>().buildingLength + gapSpace;
        lastBuildingSpawned = index;
        spawnedBuildings.Add(building);
    }

    private int GetRandomBuilding(int numOfObjects)
    {
        int index = Random.Range(0, numOfObjects);
        while (index == lastBuildingSpawned)
        {
            index = Random.Range(0, numOfObjects);
        }

        return index;
    }

    public virtual void DeleteBuilding()
    {
        Destroy(spawnedBuildings[0]);
        spawnedBuildings.RemoveAt(0);
    }
}
