
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private GameManager gameManager;
    private JetPack jetPack;

    private void Start()
    {
        jetPack = GetComponentInParent<JetPack>();
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.GetComponent<Fuel>())
        {
            Fuel fuel = hit.GetComponent<Fuel>();
            if(hit.tag == "Super")
            {
                jetPack.ActivateSuperFuel();
            }
            else
            {
                jetPack.IncrementFuel(fuel.refillAmount);
            }
            Destroy(hit);
        }

        if (hit.tag == "Death")
        {
            gameManager.LoseGame();
        }
    }
}
