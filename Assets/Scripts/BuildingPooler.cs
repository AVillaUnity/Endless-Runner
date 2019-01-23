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
        int numOfChildren = inactiveParent.childCount;
        if(numOfChildren <= 0)
        {
            return CreateBuilding();
        }
        else
        {
            int randomChild = Random.Range(0, numOfChildren);
            GameObject randomObject = inactiveParent.GetChild(randomChild).gameObject;
            while (randomObject.tag == lastBuildingSpawned)
            {
                randomChild = Random.Range(0, numOfChildren);
                randomObject = inactiveParent.GetChild(randomChild).gameObject;
            }
            Building building = randomObject.GetComponent<Building>();
            building.building = randomObject;
            building.poolerIndex = -1;

            for(int i = 0; i < objectList.Count; i++)
            {
                if(GameObject.ReferenceEquals(randomObject, objectList[i]))
                {
                    building.poolerIndex = i;
                }
            }

            lastBuildingSpawned = randomObject.tag;
            return building;
        }
    }

    public Building CreateBuilding()
    {
        foreach(GameObject buildingToSpawn in objectToSpawn)
        {
            if(buildingToSpawn.tag != lastBuildingSpawned)
            {
                GameObject newBuilding = Instantiate(buildingToSpawn, inactiveParent);

                Building building = newBuilding.GetComponent<Building>();
                building.building = newBuilding;
                objectList.Add(newBuilding);
                building.poolerIndex = objectList.Count - 1;
                newBuilding.SetActive(false);

                return building;
            }
        }
        return null;
    }

    public void DeactiveBuilding(int index)
    {
        objectList[index].SetActive(false);
        objectList[index].transform.parent = inactiveParent;
    }
}
