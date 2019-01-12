using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingPooler))]
public class Spawner : MonoBehaviour
{
    public float buildingLength = 7.5f;
    public float gapSpace = 3.0f;
    public int maxBuildingsSpawned = 16;

    private int lastBuildingSpawned = -1;
    private BuildingPooler buildingPooler;

    protected float spawnLocation = 0.0f;
    protected List<GameObject> spawnedBuildings;
    protected GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        buildingPooler = GetComponent<BuildingPooler>();
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
            ReplaceBuilding();
            DeleteBuilding();
        }
        
    }

    public virtual void SpawnBuilding()
    {
        GameObject[] objectToSpawn = buildingPooler.objectToSpawn;
        int index = GetRandomBuilding(objectToSpawn.Length);

        GameObject building = buildingPooler.GetObject(index);

        building.transform.position = transform.forward * spawnLocation;
        building.transform.rotation = Quaternion.identity;
        building.SetActive(true);

        spawnLocation += buildingLength + gapSpace;
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

    private void ReplaceBuilding()
    {
        GameObject[] objectToSpawn = buildingPooler.objectToSpawn;
        int index = GetRandomBuilding(objectToSpawn.Length);

        for(int i = 0; i < spawnedBuildings.Count; i++)
        {
            if(spawnedBuildings[i].tag == objectToSpawn[index].tag)
            {
                GameObject newBuilding = spawnedBuildings[i];
                newBuilding.transform.position = transform.forward * spawnLocation;
                newBuilding.transform.rotation = Quaternion.identity;
                newBuilding.SetActive(true);

                spawnedBuildings.Add(newBuilding);
                spawnLocation += buildingLength + gapSpace;
                lastBuildingSpawned = index;

                break;
            }
        }
    }

    public void DeleteBuilding()
    {
        spawnedBuildings[0].SetActive(false);
        spawnedBuildings.RemoveAt(0);
    }
}
