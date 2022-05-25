using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public float SteerInput { get; private set; }
    public float ThrottleInput { get; private set; }
    public float SpecialInput { get; private set; }

    private PlayerInput playerInput;

    // Store controls
    private InputAction throttleDigitalAction;
    private InputAction steerAction;
    private InputAction specialAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        throttleDigitalAction = playerInput.actions["Throttle (Digital)"];
        steerAction = playerInput.actions["Steering"];
        specialAction = playerInput.actions["Special"];
    }

    private void Update()
    {
        SteerInput = steerAction.ReadValue<float>();
        ThrottleInput = throttleDigitalAction.ReadValue<float>();
        SpecialInput = specialAction.ReadValue<float>();
    }
}
