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

    private SpawnFuel fuelSpawner;
    private bool canCheck = false;

    private void Start()
    {
        fuelSpawner = GetComponent<SpawnFuel>();
        canCheck = true;
    }

    private void OnEnable()
    {
        if (!canCheck) { return; }

        if (!fuelSpawner.HasFuel)
        {
            fuelSpawner.PickFuel();
        }
    }
}
