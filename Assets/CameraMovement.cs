using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public Transform player;
    public Transform menuCamera;
    public Transform gameCamera;

    [Range(1.0f, 5.0f)]
    public float transitionSpeed = 1.0f;

    private GameManager gameManager;
    private Vector3 offset;

    public bool Transitioning { get; private set; }

    private void Start()
    {
        gameManager = GameManager.instance;
        transform.position = menuCamera.position;
        transform.rotation = menuCamera.rotation;
        offset = gameCamera.position - player.position;
        Transitioning = false;
    }



    // Update is called once per frame
    void Update()
    {
        if(gameManager.GameStarted && !Transitioning && !AtDestination(gameCamera.position))
        {
            StartCoroutine(TransitionCamera(gameCamera));
            Transitioning = true;
        }

        if(!gameManager.GameStarted && !Transitioning && !AtDestination(menuCamera.position))
        {
            StartCoroutine(TransitionCamera(menuCamera));
            Transitioning = true;
        }
    }

    private void LateUpdate()
    {
        if (gameManager.GameStarted && !Transitioning)
        {
            Vector3 newPos = player.position + offset;
            newPos.y = gameCamera.position.y;
            transform.position = newPos;
        }
    }

    IEnumerator TransitionCamera(Transform destination)
    {
        float timeElapsed = 0.0f;

        while (transform.position != destination.position || timeElapsed >= 1.0f)
        {
            transform.position = Vector3.Lerp(transform.position, destination.position, timeElapsed);
            transform.rotation = Quaternion.Lerp(transform.rotation, destination.rotation, timeElapsed);
            timeElapsed += Time.deltaTime * (1 / transitionSpeed);
            yield return null;
        }

        Transitioning = false;
    }

    public bool CameraReadyToFollow()
    {
        return !Transitioning && gameManager.GameStarted;
    }

    private bool AtDestination(Vector3 destination)
    {
        return transform.position == destination;
    }
}
