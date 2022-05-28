using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private bool steer;
    [SerializeField] private bool invertSteer;
    [SerializeField] private bool power;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float Brake { get; set; }

    private WheelCollider wheelCollider;
    private Transform wheelTransform;

    private float nextMoveCheck;
    private float timeBetweenMoveChecks = .25f;
    private Vector3 lastPos;
    private float moveDistanceThreshold = .005f;
    private bool isMoving;
    private bool forwardMode = true;

    void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();

        nextMoveCheck = Time.time + timeBetweenMoveChecks;
        lastPos = transform.position;
    }

    void Update()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        //wheelTransform.position = pos;
        wheelTransform.rotation = rot;

        if (Time.time > nextMoveCheck)
        {
            isMoving = (Vector3.Distance(transform.position, lastPos) > moveDistanceThreshold);
            lastPos = transform.position;
            nextMoveCheck = Time.time + timeBetweenMoveChecks;

            if (!isMoving)
            {
                if (Torque > 0)
                {
                    forwardMode = true;
                } else if (Brake > 0)
                {
                    forwardMode = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            wheelCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
        }

        if (power)
        {
            wheelCollider.motorTorque = (forwardMode ? Torque : -Brake);
        }

        wheelCollider.brakeTorque = (forwardMode ? Brake : Torque);
    }
}
