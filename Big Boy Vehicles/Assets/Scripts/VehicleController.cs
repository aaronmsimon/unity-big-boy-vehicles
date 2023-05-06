using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float maxMotorTorque;

    private PlayerControls playerControls;
    private float steeringInput;
    private float motorTorqueInput;
    private Axle[] axles;

    private void Awake() {
        playerControls = new PlayerControls();
        axles = transform.Find("Axles").GetComponentsInChildren<Axle>();

        Rigidbody rb = GetComponent<Rigidbody>();
        Transform centerOfMass = transform.Find("Center of Mass");
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Update() {
        GetPlayerInput();
        PowerTrain();
    }

    private void GetPlayerInput() {
        steeringInput = playerControls.Driving.Steering.ReadValue<float>();
        motorTorqueInput = playerControls.Driving.Throttle.ReadValue<float>();
    }

    private void PowerTrain() {
        foreach (Axle axle in axles) {
            axle.SteerAngle = maxSteeringAngle * steeringInput;
            axle.MotorTorque = maxMotorTorque * motorTorqueInput;
        }
    }
}
