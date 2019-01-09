using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 9.0f;
    public float jumpSpeedBoost = 2.0f;
    public float gravity = 9.8f;
    public float defaultJumpForce = 150.0f;
    public float jumpMultiplier = 320.0f;
    public float maxJumpForce = 300.0f;


    private float jumpForce;
    private float downwardForce = 0.0f;
    private Vector3 motion;

    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpForce = defaultJumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        motion = Vector3.forward * speed;

        if (Input.GetButton("Jump"))
        {
            jumpForce += (jumpMultiplier * Time.deltaTime);
            jumpForce = Mathf.Clamp(jumpForce, defaultJumpForce, maxJumpForce);
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (controller.isGrounded)
            {
                motion.y = jumpForce;
                motion.z += jumpSpeedBoost;
                print("jumpforce: " + jumpForce);
            }
            jumpForce = defaultJumpForce;
        }

        if (controller.isGrounded)
        {
            motion.y -= gravity * Time.deltaTime;
            downwardForce = gravity;
        }
        else
        {
            downwardForce += gravity * Time.deltaTime * 2;
            motion.y -= downwardForce;
        }

        controller.Move(motion * Time.deltaTime);
    }
}
