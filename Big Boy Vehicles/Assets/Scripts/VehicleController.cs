using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private float motorTorque = 1000f;
    [SerializeField] private float maxSteer = 20f;

    [Header("Wheels")]
    [SerializeField] private Transform tireFL;
    [SerializeField] private Transform tireFR;
    [SerializeField] private Transform tireRL;
    [SerializeField] private Transform tireRR;

    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;

    [Header("Other")]
    [SerializeField] private Transform centerOfMass;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelFL.GetWorldPose(out pos, out rot);
        tireFL.position = pos;
        tireFL.rotation = rot;

        wheelFR.GetWorldPose(out pos, out rot);
        tireFR.position = pos;
        tireFR.rotation = rot * Quaternion.Euler(0,180,0);

        wheelRL.GetWorldPose(out pos, out rot);
        tireRL.position = pos;
        tireRL.rotation = rot;

        wheelRR.GetWorldPose(out pos, out rot);
        tireRR.position = pos;
        tireRR.rotation = rot * Quaternion.Euler(0, 180, 0);
    }

    private void FixedUpdate()
    {
        wheelRL.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelRR.motorTorque = Input.GetAxis("Vertical") * motorTorque;

        wheelFL.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelFR.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
    }
}
