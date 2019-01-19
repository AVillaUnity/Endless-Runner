using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float rotatingSpeed = 10.0f;
    public float refillAmount = 100.0f;

    private float playerZOffset = 1.0f;
    private float speedToDecrease = 1.0f;

    private PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        Rotate();

        if(player.transform.position.z > transform.position.z + playerZOffset)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * speedToDecrease;
            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Rotate()
    {
        //float xRotation = Time.deltaTime * rotatingSpeed;
        float yRotation = Time.deltaTime * rotatingSpeed;
        //float zRotation = Time.deltaTime * rotatingSpeed;

        transform.Rotate(0, yRotation, 0);
    }


}
