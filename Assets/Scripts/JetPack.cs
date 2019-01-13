using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class JetPack : MonoBehaviour
{
    [SerializeField]
    private float initialFuelAmount = 100;
    private PlayerMovement playerMovement;

    public float Fuel { get; set; }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.onDeath += ResetFuel;

        ResetFuel();
    }

    void ResetFuel()
    {
        Fuel = initialFuelAmount;
    }
}
