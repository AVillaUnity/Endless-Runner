using UnityEngine;

public class BuildingOffset : MonoBehaviour
{
    public Spawner buildingManager;


    private GameObject player;
    private CameraMovement cm;

    public Vector3 Offset { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cm = Camera.main.gameObject.GetComponent<CameraMovement>();
        Offset = transform.position - player.transform.position;
    }

    public void ChangePosition(Vector3 playerPosition)
    {
        transform.position = playerPosition + Offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.tag == "Building")
        {
            buildingManager.DeleteBuilding(hit.GetComponentInParent<Building>().spawnerIndex);
        }
        else if(hit.GetComponent<Fuel>())
        {
            hit.GetComponent<Fuel>().spawner.HasFuel = false;
            hit.transform.parent = hit.GetComponentInParent<ObjectPooler>().inactiveParent;
            hit.SetActive(false);
        }
    }
}
