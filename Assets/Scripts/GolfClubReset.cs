using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GolfClubReset : MonoBehaviour
{

    // trying to reset the position of the golf club if player dropped it
    [SerializeField] XRBaseInteractable golfClub;

    private Pose originalPos;

    private Rigidbody rb;

    private void OnEnable() => golfClub.selectExited.AddListener(ObjectReleased);
    private void OnDisable() => golfClub.selectExited.RemoveListener(ObjectReleased);

    // Start is called before the first frame update
    void Start()
    {
        originalPos.position = this.transform.position;
        originalPos.rotation = this.transform.rotation;

        rb = GetComponent<Rigidbody>();
        
    }

    private void ObjectReleased(SelectExitEventArgs arg0)
    {
        // rb.Sleep();
        // GetComponent<Collider>().enabled = false;
        // this.transform.position = originalPos.position;
        // this.transform.rotation = originalPos.rotation;

        // rb.WakeUp();
        // GetComponent<Collider>().enabled = true;

        // Disable physics temporarily
        rb.isKinematic = true;

        // Start coroutine to smoothly move back to original position
        StartCoroutine(MoveToOriginalPosition());
    }

    IEnumerator MoveToOriginalPosition()
    {
        float duration = 0.5f; // Adjust as needed
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsed < duration)
        {
            // Interpolate position and rotation
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, originalPos.position, t);
            transform.rotation = Quaternion.Slerp(startRot, originalPos.rotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation are exactly the original
        transform.position = originalPos.position;
        transform.rotation = originalPos.rotation;

        // Re-enable physics
        rb.isKinematic = false;
    }
}
