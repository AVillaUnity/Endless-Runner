using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building: MonoBehaviour
{
    public float buildingLength = 5.0f;

    [HideInInspector]
    public GameObject building;

    [HideInInspector]
    public int poolerIndex;

    [HideInInspector]
    public int spawnerIndex;

}
