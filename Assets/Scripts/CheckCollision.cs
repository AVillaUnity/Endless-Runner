
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Fuel>())
        {
            //print("We got Fuel");
        }

        if (other.gameObject.tag == "Building")
        {
            //print("we dead");
            playerMovement.Die();
        }
    }
}
