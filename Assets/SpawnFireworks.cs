using UnityEngine;

public class SpawnFireworks : MonoBehaviour
{
    public Transform[] fireworkSpawnPoints;
    public GameObject fireworks;

    private GameManager gameManager;

    public bool SpawnedFireworks { get; private set; }

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onReset += ResetFireworks;

        SpawnedFireworks = false;
    }

    public void Spawn()
    {
        foreach(Transform t in fireworkSpawnPoints)
        {
            GameObject fireworksDisplay = Instantiate(fireworks, t.position, Quaternion.identity, t);
            Destroy(fireworksDisplay, 5.0f);
        }

        SpawnedFireworks = true;
    }

    void ResetFireworks()
    {
        SpawnedFireworks = false;
    }
}
