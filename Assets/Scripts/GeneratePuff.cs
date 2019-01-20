using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePuff : MonoBehaviour
{
    public GameObject puff;
    public AudioClip clip;

    private CharacterController controller;
    private AudioManager audioManager;
    private PlayerMovement pm;
    private JetPack jetPack;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType<PlayerMovement>();
        audioManager = AudioManager.instance;
        controller = GameObject.FindObjectOfType<CharacterController>();
        jetPack = GameObject.FindObjectOfType<JetPack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && Input.GetButtonDown("Jump") && jetPack.CurrentFuel > 0 && pm.PlayerMoving)
        {
            Instantiate(puff, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}
