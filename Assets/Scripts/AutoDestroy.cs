using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeToDestroy = 3.0f;

    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
