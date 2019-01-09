using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public Transform player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.position + offset;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
