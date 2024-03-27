using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]

public class MovementManager : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    private float speed = 0.01f;
    private Rigidbody r;
    private CollisionDetection myColDec;
    private PhotonView myView;
    private LogisticsManager myLogistics;

    void Start()
    {
        myColDec = GetComponent<CollisionDetection>();
        myView = GetComponent<PhotonView>();
        GameObject logistics = GameObject.Find("LogisticsManager");
        myLogistics = logistics.GetComponent<LogisticsManager>();
    }
    void Update()
    {
        if(myView.IsMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            // Get the current position of the GameObject
            Vector3 currentPosition = gameObject.transform.position;

            // Calculate the new position based on input and speed
            Vector3 newPosition = new Vector3(currentPosition.x + (h * speed), currentPosition.y, currentPosition.z + (v * speed));

            // Update the position of the GameObject
            gameObject.transform.position = newPosition;

            if (myColDec.collided)
            {
                myLogistics.collisionfound = true;
                myColDec.collided = false;
            }

            if (myLogistics.iLost)
            {
                myView.RPC("communiationLost" , RpcTarget.Others, 6);
                myLogistics.resetGameplay();
            }
        }
        
    }

    [PunRPC]
    void communiationLost(int randomNumber)
    {
        Debug.Log(randomNumber);
        myLogistics.otherLost();
    }
}
