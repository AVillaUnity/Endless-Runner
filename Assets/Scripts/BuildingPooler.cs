using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPooler : ObjectPooler
{

    private string lastBuildingSpawned = "";

    public override void Awake()
    {
        base.Awake();
    }

    public Building GetBuilding()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeSelf == false && objectList[i].tag != lastBuildingSpawned)
            {
                Building building = objectList[i].GetComponent<Building>();
                building.building = objectList[i];
                building.poolerIndex = i;
                lastBuildingSpawned = objectList[i].tag;
                return building;
            }
        }
        return CreateBuilding();
    }

    public Building CreateBuilding()
    {
        foreach(GameObject buildingToSpawn in objectToSpawn)
        {
            if(buildingToSpawn.tag != lastBuildingSpawned)
            {
                GameObject newBuilding = Instantiate(buildingToSpawn, objectParent);

                Building building = newBuilding.GetComponent<Building>();
                building.building = newBuilding;
                objectList.Add(newBuilding);
                building.poolerIndex = objectList.Count - 1;
                newBuilding.SetActive(false);

                return building;
            }
        }
        print("Building not found");
        return null;
    }

    public void DeactiveBuilding(int index)
    {
        objectList[index].SetActive(false);
    }
}
