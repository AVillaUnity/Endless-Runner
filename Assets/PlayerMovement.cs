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


    public delegate void OnDeath();
    public OnDeath onDeath;

    public float DistanceTraveled { get; set; }

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        jumpForce = defaultJumpForce;
        startingYPosition = transform.position.y;
        verticalForce = 0.0f;
        speed = defaultSpeed;
        DistanceTraveled = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraMov.CameraReadyToFollow()) { return; }

        DistanceTraveled = Time.deltaTime;

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

        DistanceTraveled = 0.0f;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + controller.radius)
        {
            if(onDeath != null)
                onDeath.Invoke();
        }
    }
}
