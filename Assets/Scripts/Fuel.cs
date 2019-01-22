using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float rotatingSpeed = 10.0f;
    public float refillAmount = 100.0f;

    [HideInInspector]
    public SpawnFuel spawner;

    private float playerZOffset = 1.0f;
    private float speedToDecrease = 1.0f;
    private Vector3 startingScale;

    private PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        startingScale = transform.localScale;
    }

    void Update()
    {
        Rotate();

        if(player.transform.position.z > transform.position.z + playerZOffset)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * speedToDecrease;
            if(transform.localScale.x <= 0)
            {
                spawner.HasFuel = false;
                transform.parent = GetComponentInParent<ObjectPooler>().inactiveParent;
                transform.localScale = startingScale;
                gameObject.SetActive(false);
                
            }
        }
    }

    private void Rotate()
    {
        float yRotation = Time.deltaTime * rotatingSpeed;
        transform.Rotate(0, yRotation, 0);
    }


}
