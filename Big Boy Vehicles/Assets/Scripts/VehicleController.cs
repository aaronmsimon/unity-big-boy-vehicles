using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float motorTorque = 1000f;
    [SerializeField] private float maxSteer = 20f;
    [SerializeField] private float brakeTorque = 500f;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public float Brake { get; set; }

    private Wheel[] wheels;
    private Transform centerOfMass;
    private Rigidbody rb;

    private const float MPS_TO_MPH = 2.237f;

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();

        centerOfMass = transform.Find("CenterOfMass");

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;

    }

    void Update()
    {
        if (GameManager.Instance.PlayerManager.activeVehicle == gameObject)
        {
            Steer = GameManager.Instance.InputController.SteerInput;
            Throttle = GameManager.Instance.InputController.ThrottleInput;
            Brake = GameManager.Instance.InputController.BrakeInput;

            foreach (var wheel in wheels)
            {
                wheel.SteerAngle = Steer * maxSteer;
                wheel.Torque = Throttle * motorTorque;
                wheel.Brake = Brake * brakeTorque;
            }
            //Debug.Log(rb.velocity.magnitude * MPS_TO_MPH);
        }
    }

}
