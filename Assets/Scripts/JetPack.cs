using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class JetPack : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    public float fuelDecreaseSpeed = 1.0f;

    private GameManager gameManager;

    public float CurrentFuel { get; set; }
    public float MaxFuel { get; private set; }

    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onReset += ResetFuel;

        MaxFuel = 100.0f;
        ResetFuel();
    }

    public void ResetFuel()
    {
        CurrentFuel = MaxFuel;
    }

    public void DecreaseFuel()
    {
        CurrentFuel -= Time.deltaTime * MaxFuel * (1 / fuelDecreaseSpeed);
        if(CurrentFuel <= 0.0f) { CurrentFuel = 0.0f; }
    }
}
