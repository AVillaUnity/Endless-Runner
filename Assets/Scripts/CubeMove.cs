using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        float timeElapsed = 0.0f;
        while(timeElapsed <= 5.0f)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0.0f;

        while (timeElapsed <= 5.0f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }




    }
}
