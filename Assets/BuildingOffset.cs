using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOffset : MonoBehaviour
{

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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Building")
        {
            Destroy(other.gameObject);
        }
    }
}
