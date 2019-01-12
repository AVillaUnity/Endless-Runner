using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPooler : MonoBehaviour
{
    public int initialNumOfObjects = 16;
    public GameObject[] objectToSpawn;
    public Transform objectParent;

    private List<GameObject> objectList;

    void Start()
    {
        objectList = new List<GameObject>();

        for (int i = 0; i < initialNumOfObjects; i++)
        {
            CreateObject(objectToSpawn[i % objectToSpawn.Length]);
        }
    }

    private GameObject CreateObject(GameObject objectToSpawn)
    {
        GameObject newObject = Instantiate(objectToSpawn, objectParent);
        newObject.SetActive(false);
        objectList.Add(newObject);

        return newObject;
    }

    public GameObject GetObject(int objectIndex)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeSelf == false && objectList[i].tag == objectToSpawn[objectIndex].tag)
            {
                return objectList[i];
            }
        }

        return CreateObject(objectToSpawn[objectIndex]);
    }
}
