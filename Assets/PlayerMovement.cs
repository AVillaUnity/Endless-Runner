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

    private float jumpForce;
    private float speed;
    private bool canJump = true;
    private float verticalForce;
    private Vector3 motion;
    private float startingYPosition;
    private CameraMovement cameraMov;

    public delegate void OnDeath();
    public OnDeath onDeath;

    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraMov = GetComponent<CameraMovement>();

        jumpForce = defaultJumpForce;
        startingYPosition = transform.position.y;
        verticalForce = 0.0f;
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraMov.CameraReadyToFollow()) { return; }

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

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + controller.radius)
        {
            //print("tz: " + transform.position.z + controller.radius + ", hz: " + hit.point.z);
            if(onDeath != null)
                onDeath.Invoke();
        }
    }
}
