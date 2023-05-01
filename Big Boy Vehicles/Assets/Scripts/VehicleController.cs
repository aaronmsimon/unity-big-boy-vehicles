using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> axleInfos; // the information about each individual axle
    [SerializeField] private float maxMotorTorque; // maximum torque the motor can apply to wheel
    [SerializeField] private float maxSteeringAngle; // maximum steer angle the wheel can have

    private PlayerControls playerControls;
    private bool accelerating;
    private float steeringInput;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
        playerControls.Driving.Throttle.started += OnAcceleratePressed;
        playerControls.Driving.Throttle.canceled += OnAccelerateReleased;
    }

    private void OnDisable() {
        playerControls.Disable();
        playerControls.Driving.Throttle.started -= OnAcceleratePressed;
        playerControls.Driving.Throttle.canceled -= OnAccelerateReleased;
    }

    private void Update() {
        steeringInput = playerControls.Driving.Steering.ReadValue<float>();
    }

    private void FixedUpdate() {
        float motor = maxMotorTorque * (accelerating ? 1 : 0);
        float steering = maxSteeringAngle * steeringInput;
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    private void OnAcceleratePressed(InputAction.CallbackContext ctx) {
        accelerating = true;
    }

    private void OnAccelerateReleased(InputAction.CallbackContext ctx) {
        accelerating = false;
    }
}

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}