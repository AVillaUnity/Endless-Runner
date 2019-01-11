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
    private bool transitioning = false;
    private bool hasTransitioned = false;

    private Vector3 offset;

    private void Start()
    {
        gameManager = GameManager.instance;
        transform.position = menuCamera.position;
        transform.rotation = menuCamera.rotation;
        offset = gameCamera.position - player.position;
    }



    // Update is called once per frame
    void LateUpdate()
    {
        if(gameManager.GameStarted && !transitioning && !hasTransitioned)
        {
            StartCoroutine(TransitionCamera(gameCamera));
        }

        if(!gameManager.GameStarted && !transitioning && !hasTransitioned)
        {
            StartCoroutine(TransitionCamera(menuCamera));
        }
        
        if(gameManager.GameStarted && hasTransitioned)
        {
            Vector3 newPos = player.position + offset;
            newPos.y = gameCamera.position.y;
            transform.position = newPos;
        }
       
    }

    IEnumerator TransitionCamera(Transform destination)
    {
        transitioning = true;
        float timeElapsed = 0.0f;

        while (transform.position != destination.position || timeElapsed >= 1.0f)
        {
            transform.position = Vector3.Lerp(transform.position, destination.position, timeElapsed);
            transform.rotation = Quaternion.Lerp(transform.rotation, destination.rotation, timeElapsed);
            timeElapsed += Time.deltaTime * (1 / transitionSpeed);
            yield return null;
        }

        hasTransitioned = true;
    }

    public bool CameraReadyToFollow()
    {
        return hasTransitioned && gameManager.GameStarted;
    }
}
