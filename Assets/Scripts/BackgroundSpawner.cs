using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class BackgroundSpawner : Spawner
{
    public float playerOffset = 50.0f;

    private ObjectPooler objectPooler;
    private List<GameObject> backgroundBuildings;

    // Start is called before the first frame update
    public override void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        player = GameObject.FindGameObjectWithTag("Player");
        backgroundBuildings = new List<GameObject>();
        for (int i = 0; i < maxBuildingsSpawned; i++)
        {
            SpawnBuilding();
        }
    }

    public override void SpawnBuilding()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.z += spawnLocation;

        GameObject buildings = objectPooler.GetObject();

        buildings.transform.position = spawnPoint;
        buildings.transform.rotation = Quaternion.identity;
        buildings.SetActive(true);

        backgroundBuildings.Add(buildings);
        spawnLocation += maxBuildingLength + gapSpace;
    }

    public override void Update()
    {
        if (player.transform.position.z - playerOffset> spawnLocation - (maxBuildingsSpawned * maxBuildingLength))
        {
            SpawnBuilding();
            DeleteBuilding();
        }
    }

    public override void DeleteBuilding()
    {
        backgroundBuildings[0].SetActive(false);
        backgroundBuildings.RemoveAt(0);
    }
}
