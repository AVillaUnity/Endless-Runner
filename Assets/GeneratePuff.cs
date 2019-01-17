using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePuff : MonoBehaviour
{
    public GameObject puff;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            Instantiate(puff, transform.position, Quaternion.identity);
        }
    }
}
