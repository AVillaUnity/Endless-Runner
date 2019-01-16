using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFuel : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float percentToDropFuel = 50;
    [Range(0.0f, 100.0f)]
    public float percentToDropSuperFuel = 10;
    public GameObject fullFuel;
    public GameObject halfFuel;
    public GameObject superFuel;
    public Transform[] spawnPoints;

    void Start()
    {
        float chance = Random.Range(0.0f, 1.0f);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        if(chance >= 1 - (percentToDropFuel / 100.0f))
        {
            float fuelChance = Random.Range(0.0f, 1.0f);
            if(fuelChance >= 1 - (percentToDropSuperFuel / 100.0f)){
                Instantiate(superFuel, spawnPoints[spawnPoint].position, Quaternion.identity, spawnPoints[spawnPoint]);
            }
            else
            {
                GameObject fuelToSpawn = (Random.Range(0.0f, 1.0f) >= 0.5f) ? fullFuel : halfFuel;
                Instantiate(fuelToSpawn, spawnPoints[spawnPoint].position, Quaternion.identity, spawnPoints[spawnPoint]);
            }
        }
    }

}
