using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class JetPack : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    public float fuelDecreaseSpeed = 1.0f;
    public Slider fuelSlider;
    public Image fillImageOfSlider;

    private GameManager gameManager;
    private Color startingColor;

    public float CurrentFuel { get; set; }
    public float MaxFuel { get; private set; }

    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onReset += ResetFuel;

        startingColor = fillImageOfSlider.color;
        MaxFuel = 100.0f;
        ResetFuel();
    }

    public void ResetFuel()
    {
        CurrentFuel = MaxFuel;
        fuelSlider.value = CurrentFuel / MaxFuel;
        fillImageOfSlider.color = startingColor;
    }

    public void DecreaseFuel()
    {
        CurrentFuel -= Time.deltaTime * MaxFuel * (1 / fuelDecreaseSpeed);
        if(CurrentFuel <= 0.0f) { CurrentFuel = 0.0f; }

        fuelSlider.value = CurrentFuel / MaxFuel;
        fillImageOfSlider.color = Color.Lerp(startingColor, Color.red, 1 - (fuelSlider.value));
        
    }
}
