using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;
    private JetPack jetPack;
    private Player player;

    private void Start()
    {
        jetPack = GetComponentInParent<JetPack>();
        player = GetComponentInParent<Player>();
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
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
                audioManager.Play("Power");
            }
            else
            {
                jetPack.IncrementFuel(fuel.refillAmount);
                audioManager.Play("Fuel");
            }
            fuel.spawner.HasFuel = false;
            hit.transform.parent = hit.GetComponentInParent<ObjectPooler>().inactiveParent;
            hit.SetActive(false);
        }

        if (hit.tag == "Death")
        {
            player.Die();
            gameManager.LoseGame();
        }
    }
}
