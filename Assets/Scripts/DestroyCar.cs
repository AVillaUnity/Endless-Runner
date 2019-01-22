
using UnityEngine;

public class DestroyCar : MonoBehaviour
{

    public Transform inActiveParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DriveCar>())
        {
            other.gameObject.SetActive(false);
            other.transform.parent = inActiveParent;
        }
    }
}
