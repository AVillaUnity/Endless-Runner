using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GeneratePuff : MonoBehaviour
{
    public AudioClip clip;
    public ObjectPooler pooler;

    private List<GameObject> activatedPuffs;
    private CharacterController controller;
    private AudioManager audioManager;
    private PlayerMovement pm;
    private JetPack jetPack;
    // Start is called before the first frame update
    void Start()
    {
        activatedPuffs = new List<GameObject>();
        pm = GameObject.FindObjectOfType<PlayerMovement>();
        audioManager = AudioManager.instance;
        controller = GameObject.FindObjectOfType<CharacterController>();
        jetPack = GameObject.FindObjectOfType<JetPack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && CrossPlatformInputManager.GetButtonDown("Jump") && jetPack.CurrentFuel > 0 && pm.PlayerMoving)
        {
            ActivatePuff();
            StartCoroutine(DeactivatePuff());
        }
    }

    private void ActivatePuff()
    {
        GameObject puff = pooler.GetObject();
        puff.transform.position = transform.position;
        puff.transform.rotation = Quaternion.identity;
        puff.transform.parent = pooler.activeParent;
        puff.SetActive(true);
        activatedPuffs.Add(puff);
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    IEnumerator DeactivatePuff()
    {
        yield return new WaitForSeconds(2.0f);

        activatedPuffs[0].transform.parent = pooler.inactiveParent;
        activatedPuffs[0].SetActive(false);
        activatedPuffs.RemoveAt(0);
    }
}
