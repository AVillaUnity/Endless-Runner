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
    public float minRotationSpeed = 10.0f;
    private float minRotationAngle = 0.0f;
    public float maxRotationDegree = 60.0f;
    public CameraMovement cameraMov;


    private float jumpForce = 0.0f;
    private float forwardForce = 0.0f;
    private float rotationAngle;
    private float rotationSpeed;
    private float gravity;
    private float speed;
    private Vector3 motion;
    private float startingYPosition;
    private CharacterController controller;
    private Animator animator;
    private float lastZPosition;
    private JetPack jetPack;
    private GameManager gameManager;
    private Player player;

    public float DistanceTraveled { get; set; }
    public bool PlayerMoving { get; private set; }
    public Vector3 StartingPosition { get; set; }

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        gameManager = GameManager.instance;
        controller = GetComponent<CharacterController>();
        jetPack = GetComponent<JetPack>();

        startingYPosition = transform.position.y;
        StartingPosition = transform.position;
        speed = defaultSpeed;
        DistanceTraveled = 0.0f;
        PlayerMoving = false;
        lastZPosition = transform.position.z;
        gravity = initialGravity;

        rotationSpeed = minRotationSpeed;
        rotationAngle = minRotationAngle;
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

        if (player.IsDead) { return; }

        CalculateDistance();
        CalculateVerticalForce();
        CalculateForwardForce();

        if (Input.GetAxis("Horizontal") > 0.0f && Input.GetButton("Jump"))
        {
            speed += Time.deltaTime * 20;
        }
        else
        {
            speed -= Time.deltaTime * 15;
        }

        speed = Mathf.Clamp(speed, defaultSpeed, defaultSpeed + 5);

        motion = transform.forward * forwardForce * speed;
        motion.y -= (gravity * Time.deltaTime) - (jumpForce * Time.deltaTime);



        controller.Move(motion * Time.deltaTime);

        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.Clamp(newPosition.y, 0, maxHeight);
        transform.position = newPosition;
    }

    private void CalculateVerticalForce()
    {
        float hasFuel = (jetPack.CurrentFuel > 0) ? 1.0f : 0.0f;
        jumpForce += hasFuel * ((Time.deltaTime * Input.GetAxis("Jump")) + (initialJumpForce * Input.GetAxisRaw("Jump")));

        if (Input.GetButton("Jump") && jetPack.CurrentFuel > 0)
        {
            gravity -= Time.deltaTime * initialGravity;
            animator.SetBool("Jump", true);

            jetPack.DecreaseFuel();
        }
        else
        {
            gravity = initialGravity;
            Fall();
        }

        jumpForce = (jumpForce > 0) ? jumpForce - (gravity * Time.deltaTime) : 0.0f;
        jumpForce = Mathf.Clamp(jumpForce, 0, maxVerticalForce);
    }

    private void Fall()
    {
        animator.SetBool("Jump", false);

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit raycastHit = new RaycastHit();

        if(Physics.Raycast(ray, out raycastHit))
        {
            if(raycastHit.distance > 1.75)
            {
                animator.MatchTarget(raycastHit.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0f, .05f);
            }
        }
    }

    private void CalculateForwardForce()
    {
        float forwardGravity = 3;
        float forwardVelocity = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1.0f);
        
        forwardForce += Time.deltaTime * forwardGravity * forwardVelocity;
        forwardForce -= Time.deltaTime * forwardGravity / 3;
        forwardForce = Mathf.Clamp(forwardForce, 0.0f, 1.0f);
        animator.SetFloat("Speed", forwardForce);
    }

    private void CalculateDistance()
    {
        if (transform.position.z > lastZPosition)
        {
            DistanceTraveled = transform.position.z - lastZPosition;
            lastZPosition = transform.position.z;
        }
        else
        {
            DistanceTraveled = 0;
        }
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

        jumpForce = 0.0f;
        forwardForce = 0.0f;

        // Used to calculate where the highscore object will be placed
        StartingPosition = transform.position;

        DistanceTraveled = 0.0f;
        player.IsDead = false;
    }
}
