using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : Spawner
{
    public GameObject backgroundBuildings;
    public float playerOffset = 50.0f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SpawnBuilding()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.z += spawnLocation;
        GameObject buildings = Instantiate(backgroundBuildings, spawnPoint, Quaternion.identity, transform);
        spawnedBuildings.Add(buildings);
        spawnLocation += buildingLength + gapSpace;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player.transform.position.z - playerOffset> spawnLocation - (maxBuildingsSpawned * buildingLength))
        {
            SpawnBuilding();
            DeleteBuilding();
        }
    }
}
