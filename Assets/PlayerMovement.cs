using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 9.0f;
    public float gravity = 9.8f;
    public float defaultJumpForce = 150.0f;
    public float jumpMultiplier = 320.0f;
    public float maxJumpForce = 300.0f;

    private float jumpForce;
    private bool canJump = true;
    private float verticalForce;
    private Vector3 motion;

    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpForce = defaultJumpForce;
        verticalForce = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
}
