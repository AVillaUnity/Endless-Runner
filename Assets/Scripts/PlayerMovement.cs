using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10.0f;
    public float gravity = 5800.0f;
    public float verticalForce = 6500.0f;
    public float maxJumpForce = 7500.0f;
    public CameraMovement cameraMov;


    private float jumpForce = 0.0f;
    private float speed;
    private bool canJump = true;
    private Vector3 motion;
    private float startingYPosition;
    private CharacterController controller;
    private float lastZPosition;
    private JetPack jetPack;

    public float DistanceTraveled { get; set; }
    public bool PlayerMoving { get; private set; }
    public Vector3 StartingPosition { get; set; }

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jetPack = GetComponent<JetPack>();

        startingYPosition = transform.position.y;
        StartingPosition = transform.position;
        speed = defaultSpeed;
        DistanceTraveled = 0.0f;
        PlayerMoving = false;
        lastZPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraMov.CameraReadyToFollow())
        {
            PlayerMoving = false;
            return;
        }
        PlayerMoving = true;

        DistanceTraveled = transform.position.z - lastZPosition;
        lastZPosition = transform.position.z;

        motion = Vector3.forward * speed;

        // Started Jump change

        jumpForce += Time.deltaTime * verticalForce * Input.GetAxisRaw("Jump") * (jetPack.CurrentFuel / jetPack.MaxFuel);
        jumpForce = Mathf.Clamp(jumpForce, 0, maxJumpForce);

        print(jumpForce);

        if (Input.GetButton("Jump"))
        {
            jetPack.DecreaseFuel();
        }

        if(jumpForce > 0)
        {
            jumpForce -= gravity * Time.deltaTime;
        }

        motion.y -= (gravity * Time.deltaTime) - (jumpForce * Time.deltaTime);

        controller.Move(motion * Time.deltaTime);
    }

    public void IncrementSpeed()
    {
        speed++;
    }

    public void ResetPlayer()
    {
        speed = defaultSpeed;

        Vector3 newPosition = transform.position;
        newPosition.y = startingYPosition;
        transform.position = newPosition;

        // Used to calculate where the highscore object will be placed
        StartingPosition = transform.position;

        DistanceTraveled = 0.0f;

    }
}
