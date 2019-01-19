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
    public Color superFuelColor;

    private GameManager gameManager;
    private AudioSource audioSource;
    private Color startingColor;
    private bool goingToMax = false;
    private CharacterController controller;
    private IEnumerator increaseToMax;

    public float CurrentFuel { get; set; }
    public float MaxFuel { get; private set; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.instance;
        controller = GetComponent<CharacterController>();

        gameManager.onReset += ResetFuel;

        increaseToMax = IncreaseToMax();
        startingColor = fillImageOfSlider.color;
        MaxFuel = 200.0f;
        ResetFuel();
    }

    public void ResetFuel()
    {
        StopCoroutine(increaseToMax);
        CurrentFuel = MaxFuel / 2;
        fuelSlider.value = CurrentFuel / MaxFuel;
        fillImageOfSlider.color = startingColor;
        goingToMax = false;
    }

    public void DecreaseFuel()
    {
        if(goingToMax) { return; }

        CurrentFuel -= Time.deltaTime * (MaxFuel / 2) * (1 / fuelDecreaseSpeed);
        if(CurrentFuel <= 0.0f)
        {
            CurrentFuel = 0.0f;
            
        }

        fuelSlider.value = CurrentFuel / MaxFuel;
        fillImageOfSlider.color = Color.Lerp(startingColor, Color.red, 1 - (CurrentFuel / (MaxFuel / 2)));
    }

    public void IncrementFuel(float amount)
    {
        if(CurrentFuel >= (MaxFuel / 2))
        {
            return;
        }
        CurrentFuel = Mathf.Clamp(CurrentFuel + amount, 0.0f, MaxFuel / 2);
        fuelSlider.value = CurrentFuel / MaxFuel;
        fillImageOfSlider.color = Color.Lerp(startingColor, Color.red, 1 - (CurrentFuel / (MaxFuel / 2)));
    }

    public void ActivateSuperFuel()
    {
        if(goingToMax) { return; }

        goingToMax = true;
        StartCoroutine(increaseToMax);
    }

    private IEnumerator IncreaseToMax()
    {
        float timeElapsed = 0.0f;

        while(CurrentFuel < MaxFuel || timeElapsed >= 10.0f)
        {
            CurrentFuel += Time.deltaTime * (MaxFuel / 2) * (1 / fuelDecreaseSpeed);
            if(CurrentFuel > MaxFuel) { CurrentFuel = MaxFuel; }
            timeElapsed += Time.deltaTime;

            fuelSlider.value = CurrentFuel / MaxFuel;
            fillImageOfSlider.color = Color.Lerp(startingColor, superFuelColor, fuelSlider.value);
            yield return null;
        }
        goingToMax = false;
        increaseToMax = IncreaseToMax();
    }
}
