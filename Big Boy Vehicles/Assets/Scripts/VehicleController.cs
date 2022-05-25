using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float motorTorque = 1000f;
    [SerializeField] private float maxSteer = 20f;
    
    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Wheel[] wheels;
    private Transform centerOfMass;
    private Rigidbody rb;

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();

        centerOfMass = transform.Find("CenterOfMass");

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        
    }

    void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }

}
