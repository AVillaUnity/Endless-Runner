using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject disableOnDeath;
    public GameObject explosion;

    private JetPack jetPack;
    private CharacterController controller;
    private GameManager gameManger;
    private float timeElapsed = 0.0f;

    public bool IsDead { get; set; }

    private void Start()
    {
        gameManger = GameManager.instance;
        controller = GetComponent<CharacterController>();
        jetPack = GetComponent<JetPack>();
    }

    private void Update()
    {
        if(controller.isGrounded && jetPack.CurrentFuel <= 0.0f && !IsDead)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= 1.0f)
            {
                timeElapsed = 0.0f;
                Die();
                gameManger.LoseGame();
            }
        }
        else
        {
            timeElapsed = 0.0f;
        }
    }


    public void Die()
    {
        IsDead = true;
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        disableOnDeath.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        disableOnDeath.SetActive(true);
    }
}
