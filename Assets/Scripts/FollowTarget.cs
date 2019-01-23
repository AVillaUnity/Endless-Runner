using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;

    private float yDirection;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        yDirection = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = target.position + offset;
        newPosition.y = yDirection;
        transform.position = newPosition;
    }
}
