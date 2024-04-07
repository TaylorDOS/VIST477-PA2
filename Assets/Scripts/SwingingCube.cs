using UnityEngine;

public class SwingingCube : MonoBehaviour
{
    public float swingSpeed = 1f; // Speed of swinging motion
    public float swingRange = 1f; // Range of swing motion along the Z-axis
    public float startDelay = 2f;

    private bool swingingStarted = false;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        if (!swingingStarted && Time.time - startTime >= startDelay)
        {
            swingingStarted = true;
        }

        if (swingingStarted)
        {
            // Calculate the position offset using sine function to create a swinging motion
        float zOffset = Mathf.Sin(Time.time * swingSpeed) * swingRange;

        // Update the position of the cube along the Z-axis
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+ zOffset);
        }
        
    }
}
