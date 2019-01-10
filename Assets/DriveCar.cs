using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCar : MonoBehaviour
{
    public float speed = 10.0f;
    public float timeToDestroy = 60.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, timeToDestroy);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
}
