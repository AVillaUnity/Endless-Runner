using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCar : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody rb;
    private GameObject[] carBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().pitch = Random.Range(1f, 3f);
        ChangeColor();
    }

    private void ChangeColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject bodyPart = transform.GetChild(i).gameObject;
            if (bodyPart.tag == "Car")
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                bodyPart.GetComponent<Renderer>().material.color = randomColor;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    
}
