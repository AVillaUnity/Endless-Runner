using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform carHolder;
    public float rollSpawnDiceEvery = 2.0f;

    private float timeElapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= rollSpawnDiceEvery)
        {
            float randomNum = Random.Range(0.0f, 1.0f);
            if (randomNum > 0.5f)
            {
                Instantiate(carPrefab, transform.position, transform.rotation, carHolder);
            }
            timeElapsed = 0.0f;
        }
        
    }
}
