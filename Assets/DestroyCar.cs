
using UnityEngine;

public class DestroyCar : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DriveCar>())
        {
            Destroy(other.gameObject);
        }
    }
}
