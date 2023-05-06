using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Axle : MonoBehaviour
{
    enum SteeringType { None, Regular, Inverse }

    [SerializeField] private SteeringType steeringType;
    [SerializeField] private bool powered;

    public float SteerAngle { private get; set; }
    public float MotorTorque { private get; set; }

    private WheelCollider[] wheels;

    private void Awake() {
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    private void Update() {
        foreach (WheelCollider wheel in wheels) {
            wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
            Transform wheelMesh = wheel.GetComponentInChildren<Transform>();
            wheelMesh.rotation = rot;
        }
    }

    private void FixedUpdate() {
        foreach (WheelCollider wheel in wheels) {
            if (steeringType != SteeringType.None) {
                wheel.steerAngle = SteerAngle * (steeringType == SteeringType.Regular ? 1 : -1);
            }
            if (powered) {
                wheel.motorTorque = MotorTorque;
            }
        }
    }
}
