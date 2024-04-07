using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_interaction : MonoBehaviour
{
    //public GameObject ballPrefab; // Reference to the ball prefab for respawning
    // public GameObject playerPrefab; // Reference to the player prefab

    private Rigidbody rb; // Rigidbody component of the golf ball
    private Vector3 initialPosition; // Initial position of the ball
    public GameObject player; // Reference to the player GameObject
    private bool playerTeleported = false; // Flag to track whether the player has been teleported near the ball

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position; // Capture the initial position

        // Spawn the player at the initial position
        // player = Instantiate(playerPrefab, initialPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5) // Check if the ball's y position is less than -5
        {
            Debug.Log("You have successfully put the ball!");
            RespawnBall();
            playerTeleported = false; // Reset the playerTeleported flag
        }
        
        // Check if the ball has stopped moving and the player hasn't been teleported yet
        if (rb.velocity.magnitude < 0.01f && !playerTeleported)
        {
            // Calculate the direction from the player's current position to the ball's position
            Vector3 directionToBall = (transform.position - player.transform.position).normalized;

            // Calculate the new position for the player along the direction towards the ball
            Vector3 newPosition = transform.position - directionToBall * 5f;

            // Fix x and z components of the player's rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToBall, Vector3.up); // "up" vector maintains eye level
            Vector3 euler = targetRotation.eulerAngles;
            euler.x = player.transform.rotation.eulerAngles.x; // Keep x fixed
            euler.z = player.transform.rotation.eulerAngles.z; // Keep z fixed
            targetRotation = Quaternion.Euler(euler);

            // Update the player's position
            player.transform.position = newPosition;

            // Update the player's rotation
            player.transform.rotation = targetRotation;

            playerTeleported = true; // Set the playerTeleported flag to false
        }

        if (rb.velocity.magnitude > 0.01f && playerTeleported)
        {
            playerTeleported = false;
        }
    }

    // Respawn the ball
    private void RespawnBall()
    {
        //GameObject newBall = Instantiate(ballPrefab, initialPosition, Quaternion.identity); // Respawn ball at the initial position
        //Destroy(gameObject); // Destroy the current ball
        transform.position = initialPosition;
    }
}
