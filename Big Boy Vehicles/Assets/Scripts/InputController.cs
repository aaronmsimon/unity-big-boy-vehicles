using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public float SteerInput { get; private set; }
    public float ThrottleInput { get; private set; }
    public float BrakeInput { get; private set; }
    public float SpecialInput { get; private set; }

    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();       
    }

    private void Update()
    {
        // Vehicle Controls
        SteerInput = playerInput.actions["Steering"].ReadValue<float>();
        ThrottleInput = playerInput.actions["Throttle"].ReadValue<float>();
        BrakeInput = playerInput.actions["Brake"].ReadValue<float>();
        SpecialInput = playerInput.actions["Special"].ReadValue<float>();
    }

}
