using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFuel : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float percentToDropFuel = 50;
    [Range(0.0f, 100.0f)]
    public float percentToDropSuperFuel = 10;
    public Transform[] spawnPoints;

    [HideInInspector]
    public ObjectPooler pooler;

    public bool HasFuel { get; set; }

    void Start()
    {
        pooler = GameObject.FindGameObjectWithTag("Fuel Pooler").GetComponent<ObjectPooler>();
        HasFuel = false;
        PickFuel();
    }

    public void PickFuel()
    {
        float chance = Random.Range(0.0f, 1.0f);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        if (chance >= 1 - (percentToDropFuel / 100.0f))
        {
            float fuelChance = Random.Range(0.0f, 1.0f);
            if (fuelChance >= 1 - (percentToDropSuperFuel / 100.0f))
            {
                PlaceFuel("Super", spawnPoint);
            }
            else
            {
                string fuelToSpawn = (Random.Range(0.0f, 1.0f) >= 0.5f) ? "Full" : "Half";
                PlaceFuel(fuelToSpawn, spawnPoint);
            }

            HasFuel = true;
        }
    }

    void PlaceFuel(string fuel, int spawnPoint)
    {
        GameObject f = pooler.GetObject(fuel);
        f.transform.position = spawnPoints[spawnPoint].position;
        f.transform.rotation = Quaternion.identity;
        f.transform.parent = pooler.activeParent;
        f.GetComponent<Fuel>().spawner = this;
        f.SetActive(true);
    }
}
