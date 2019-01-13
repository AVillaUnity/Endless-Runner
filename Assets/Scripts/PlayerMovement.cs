using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10.0f;
    public float initialGravity = 2500.0f;
    public float maxVerticalForce = 2000.0f;
    public float initialJumpForce = 2000.0f;
    public float maxHeight = 17.0f;
    public CameraMovement cameraMov;


    private float jumpForce = 0.0f;
    private float gravity;
    private float speed;
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
        gravity = initialGravity;
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

        float hasFuel = (jetPack.CurrentFuel > 0) ? 1.0f : 0.0f;
        jumpForce += hasFuel * ((Time.deltaTime * Input.GetAxis("Jump")) + (initialJumpForce * Input.GetAxisRaw("Jump")));
        jumpForce = Mathf.Clamp(jumpForce, 0, maxVerticalForce);

        if (Input.GetButton("Jump") && jetPack.CurrentFuel > 0)
        {
            gravity -= Time.deltaTime * initialGravity;
            jetPack.DecreaseFuel();
        }
        else
        {
            gravity = initialGravity;
        }

        jumpForce = (jumpForce > 0) ? jumpForce - (gravity * Time.deltaTime * 2) : 0.0f;

        motion.y -= (gravity * Time.deltaTime) - (jumpForce * Time.deltaTime);

        

        controller.Move(motion * Time.deltaTime);

        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.Clamp(newPosition.y, 0, maxHeight);
        transform.position = newPosition;

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
