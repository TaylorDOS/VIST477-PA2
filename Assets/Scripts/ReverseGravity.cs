using System.Collections;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public float upwardForce = 10f; // The force applied to make the ball fly upwards
    public float upwardDuration = 1f; // Duration for which the ball stays up
    private Rigidbody rb;
    private bool isFlying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (!isFlying)
            {
                // Apply upward force
                rb.useGravity = false;
                rb.velocity = Vector3.zero; // Reset velocity
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

                // Start coroutine to reset gravity after upwardDuration
                StartCoroutine(ResetGravity());
            }
        }
    }

    IEnumerator ResetGravity()
    {
        isFlying = true;
        yield return new WaitForSeconds(upwardDuration);

        // Revert to normal gravity
        rb.useGravity = true;
        isFlying = false;
    }
}