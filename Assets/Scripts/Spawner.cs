using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxBuildingLength = 7.5f;
    public float gapSpace = 3.0f;
    public int maxBuildingsSpawned = 16;
    public GameObject objectPooler;

    private BuildingPooler pooler;
    private List<Building> spawnedBuildings;
    private float offset = 10.0f;


    protected float spawnLocation = 0.0f;
    protected GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        pooler = objectPooler.GetComponent<BuildingPooler>();
        player = GameObject.FindGameObjectWithTag("Player");
        spawnedBuildings = new List<Building>();
        for(int i = 0; i < maxBuildingsSpawned; i++)
        {
            SpawnBuilding();
        }    
    }

    public virtual void Update()
    {
        if(player.transform.position.z - offset > spawnLocation - (maxBuildingsSpawned * maxBuildingLength))
        {
            SpawnBuilding();
            DeleteBuilding();
        }
        
    }

    public virtual void SpawnBuilding()
    {
        Building pooledBuilding = pooler.GetBuilding();
        GameObject buildingToSpawn = pooledBuilding.building;

        buildingToSpawn.transform.position = transform.forward * spawnLocation;
        buildingToSpawn.transform.rotation = Quaternion.identity;
        buildingToSpawn.transform.parent = pooler.activeParent;
        buildingToSpawn.SetActive(true);

        spawnLocation += pooledBuilding.buildingLength + gapSpace;
        spawnedBuildings.Add(pooledBuilding);
        pooledBuilding.spawnerIndex = spawnedBuildings.Count - 1;
    }

    public virtual void DeleteBuilding()
    {
        pooler.DeactiveBuilding(spawnedBuildings[0].poolerIndex);
        spawnedBuildings.RemoveAt(0);
        RefreshIndex(0);
    }

    public void DeleteBuilding(int index)
    {
        pooler.DeactiveBuilding(spawnedBuildings[index].poolerIndex);
        spawnedBuildings.RemoveAt(index);
        RefreshIndex(index);
        SpawnBuilding();
    }

    private void RefreshIndex(int startingIndex)
    {
        for(int i = startingIndex; i < spawnedBuildings.Count; i++)
        {
            spawnedBuildings[i].spawnerIndex--;
        }
    }
}
