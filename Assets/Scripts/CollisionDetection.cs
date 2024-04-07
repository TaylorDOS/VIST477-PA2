using System.Collections;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject currentGameSetup;
    [SerializeField] public GameObject player;
    private Rigidbody rb;
    private Vector3 ballInitialPosition;
    private bool playerTeleported = true;
    private GameLogistic manager;
    private bool canIncrementScore = true;
    private float scoreIncrementCooldown = 1.0f;
    private bool newGame = false;
    public enum Type{
        Neptune,
        Earth,
        Mars
    };
    [SerializeField] public Type location = Type.Earth;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballInitialPosition = transform.position;
        GameObject gameLogistic = GameObject.Find("Game Logistic");
        if (gameLogistic != null)
            {
                manager = gameLogistic.GetComponent<GameLogistic>();
            }
            else
            {
                Debug.LogError("GameObject named 'LogisticsManager' not found.");
            }
    }

    void Update()
    {
        // ball go into hole
        if (transform.position.y < -5)
        {
            manager.updateCourseStatus(location.ToString());
            GameEnded();
            playerTeleported = false;
        }
        
        if (rb.velocity.magnitude < 0.1f && !playerTeleported)
        {
            Vector3 directionToBall = (transform.position - player.transform.position).normalized;

            Vector3 newPosition = transform.position - directionToBall * 5f;

            Quaternion targetRotation = Quaternion.LookRotation(directionToBall, Vector3.up);
            Vector3 euler = targetRotation.eulerAngles;
            euler.x = player.transform.rotation.eulerAngles.x;
            euler.z = player.transform.rotation.eulerAngles.z;
            targetRotation = Quaternion.Euler(euler);

            player.transform.position = newPosition;

            player.transform.rotation = targetRotation;

            playerTeleported = true;
        }

        if (rb.velocity.magnitude > 0.1f && playerTeleported)
        {
            playerTeleported = false;
        }
    }

    private void GameEnded()
    {
        // prompt user to move to next course when hitting hole
        manager.moveToNextCourse();
        // reset the ball to orginal position
        transform.position = ballInitialPosition;
        newGame = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Club" && canIncrementScore)
        {
            if (newGame)
            {
                manager.resetScore();
                newGame = false;
            }
            manager.addScore();
            canIncrementScore = false;
            StartCoroutine(ResetScoreIncrementCooldown());
        }
        
    }
    private IEnumerator ResetScoreIncrementCooldown()
    {
        yield return new WaitForSeconds(scoreIncrementCooldown);
        canIncrementScore = true;
    }
}
