using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10.0f;
    public float gravity = 5800.0f;
    public float defaultJumpForce = 6500.0f;
    public float jumpMultiplier = 6500.0f;
    public float maxJumpForce = 7500.0f;
    public CameraMovement cameraMov;


    private float jumpForce;
    private float speed;
    private bool canJump = true;
    private float verticalForce;
    private Vector3 motion;
    private float startingYPosition;
    private CharacterController controller;
    private GameManager gameManager;
    private float lastZPosition;


    public delegate void OnDeath();
    public OnDeath onDeath;

    public float DistanceTraveled { get; set; }
    public bool PlayerMoving { get; private set; }
    public Vector3 StartingPosition { get; set; }

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = GameManager.instance;

        jumpForce = defaultJumpForce;
        startingYPosition = transform.position.y;
        StartingPosition = transform.position;
        verticalForce = 0.0f;
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

        if (Input.GetButton("Jump"))
        {
            jumpForce += Time.deltaTime * jumpMultiplier;
            jumpForce = Mathf.Clamp(jumpForce, defaultJumpForce, maxJumpForce);

            if (canJump)
            {
                verticalForce = jumpForce;
            }
        }

        if (Input.GetButtonUp("Jump") || jumpForce >= maxJumpForce)
        {
            canJump = false;
            jumpForce = defaultJumpForce;
        }

        if (controller.isGrounded)
        {
            canJump = true;
        }

        if(verticalForce > 0)
        {
            verticalForce -= gravity * Time.deltaTime;
        }
        if(verticalForce < 0)
        {
            verticalForce = 0;
        }

        motion.y -= (gravity * Time.deltaTime) - (verticalForce * Time.deltaTime);

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + controller.radius)
        {
            if(onDeath != null)
            {
                onDeath.Invoke();
            }
        }
    }
}
