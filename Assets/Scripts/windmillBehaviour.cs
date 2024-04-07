using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 90f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around the y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
