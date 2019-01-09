using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildingPrefabs;

    public float buildingLength = 5.0f;
    public float gapSpace = 0.5f;
    public int maxBuildingsSpawned = 3;

    protected float spawnLocation = 0.0f;
    private int lastBuildingSpawned = -1;
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
        if(player.transform.position.z > spawnLocation - (maxBuildingsSpawned * buildingLength))
        {
            SpawnBuilding();
            DeleteBuilding();
        }
        
    }

    public virtual void SpawnBuilding()
    {
        int index = Random.Range(0, buildingPrefabs.Length);
        while(index == lastBuildingSpawned)
        {
            index = Random.Range(0, buildingPrefabs.Length);
        }
        GameObject building = Instantiate(buildingPrefabs[index], transform.forward * spawnLocation, Quaternion.identity, transform);
        spawnLocation += buildingLength + gapSpace;
        lastBuildingSpawned = index;
        spawnedBuildings.Add(building);
    }

    public void DeleteBuilding()
    {
        Destroy(spawnedBuildings[0]);
        spawnedBuildings.RemoveAt(0);
    }
}
