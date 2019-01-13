using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float rotatingSpeed = 10.0f;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float xRotation = Time.deltaTime * rotatingSpeed;
        float yRotation = Time.deltaTime * rotatingSpeed;
        float zRotation = Time.deltaTime * rotatingSpeed;

        transform.Rotate(xRotation, yRotation, zRotation, Space.World);
    }
}
