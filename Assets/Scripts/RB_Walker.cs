using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]

public class RB_Walker : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    public float speed = 20.0f;
    private InputData inputData;
    private float maxVelocityChange = 10.0f;
    private Rigidbody r;
    private float xInput;
    private float yInput;

    void Awake()
    {
        inputData = GetComponent<InputData>();
        r = GetComponent<Rigidbody>();
        r.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    void Update()
    {
        if(inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;
            }
            else{
                xInput = Input.GetAxis("Horizontal");
                yInput = Input.GetAxis("Vertical");
            }
    }

    void FixedUpdate()
    {
        Vector3 forwardDir = Vector3.Cross(transform.up, -playerCamera.transform.right).normalized;
        Vector3 rightDir = Vector3.Cross(transform.up, playerCamera.transform.forward).normalized;
        Vector3 targetVelocity = forwardDir * yInput * speed + rightDir * xInput * speed;
        Vector3 velocity = transform.InverseTransformDirection(r.velocity);
        velocity.y = 0;
        velocity = transform.TransformDirection(velocity);
        Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        velocityChange = transform.TransformDirection(velocityChange);

        r.AddForce(velocityChange, ForceMode.VelocityChange);

    }
}
