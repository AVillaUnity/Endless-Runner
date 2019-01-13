using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFuel : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float percentToDropFuel = 50;
    public GameObject fuel;
    public Transform[] spawnPoints;

    void Start()
    {
        float chance = Random.Range(0.0f, 1.0f);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        if(chance >= 1 - (percentToDropFuel / 100.0f))
        {
            Instantiate(fuel, spawnPoints[spawnPoint].position, Quaternion.identity, spawnPoints[spawnPoint]);
        }
    }

}
