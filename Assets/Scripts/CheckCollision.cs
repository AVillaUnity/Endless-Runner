
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
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
            //gameManager.LoseGame();
        }
    }
}
