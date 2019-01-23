using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float rollSpawnDiceEvery = 2.0f;
    public ObjectPooler objectPooler;

    private float timeElapsed = 0.0f;
    

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= rollSpawnDiceEvery)
        {
            float randomNum = Random.Range(0.0f, 1.0f);
            if (randomNum > 0.5f)
            {
                SpawnCar();
            }
            timeElapsed = 0.0f;
        }
        
    }

    private void SpawnCar()
    {
        GameObject car = objectPooler.GetObject();
        car.transform.position = transform.position;
        car.transform.rotation = transform.rotation;
        car.transform.parent = objectPooler.activeParent;
        car.SetActive(true);
    }

}
