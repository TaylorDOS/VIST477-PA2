using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class GazeToTeleport : MonoBehaviour
{
    public GameObject currentGameSetup;
    public GameObject currentPlayer;
    public GameObject ball;
    public TextMeshProUGUI canvasText;
    public AudioClip bgm;
    private AudioSource audioSource;
    public enum Type{
        Neptune,
        Earth,
        Mars
    };
    [SerializeField] public Type location = Type.Earth;

    [SerializeField] public float gravityForce;

    private InputData inputData;
    private bool gazed = false;
    private Vector3 originalPos;
    private GameLogistic manager;


    void Start()
    {
        GameObject gameLogistic = GameObject.Find("Game Logistic");
        if (gameLogistic != null)
        {
            manager = gameLogistic.GetComponent<GameLogistic>();
        }
        else
        {
            Debug.LogError("GameObject named 'LogisticsManager' not found.");
        }
        audioSource = currentPlayer.GetComponent<AudioSource>();
        inputData = FindObjectOfType<InputData>();
        originalPos = ball.transform.position;
    }

    void Update()
    {
        if (gazed)
        {
            if (inputData.leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonXPressed) && buttonXPressed)
            {
                TeleportToGazedObject();
                GameObject[] setupObjects = GameObject.FindGameObjectsWithTag("Setup");
                foreach (GameObject obj in setupObjects)
                {
                    obj.SetActive(false);
                }
                currentGameSetup.SetActive(true);
                audioSource.clip = bgm;
                audioSource.Play();
            }
        }
    }
    private Vector3 offset = new Vector3(0f, 10f, -5f);
    private void TeleportToGazedObject()
    {
        
        currentPlayer.transform.position = originalPos + offset;
        ball.transform.position = originalPos;
        Physics.gravity = new Vector3(0, gravityForce, 0);
        manager.updateCourse(location.ToString());
    }

    private void OnEnable()
    {
        gazed = true;
    }

    private void OnDisable()
    {
        gazed = false;
    }
}
