
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
        if (other.gameObject.GetComponent<Fuel>())
        {
            jetPack.ResetFuel();
            Destroy(other.gameObject);
            //print("We got Fuel");
        }

        if (other.gameObject.tag == "Building")
        {
            //print("we dead");
            //gameManager.LoseGame();
        }
    }
}
