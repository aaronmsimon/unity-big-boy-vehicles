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

    public float SpawnRockInput { get; private set; }

    private PlayerInput playerInput;

    // Store controls
    private InputAction steerAction;
    private InputAction throttleAction;
    private InputAction brakeAction;
    private InputAction specialAction;

    private InputAction spawnRocks;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        steerAction = playerInput.actions["Steering"];
        throttleAction = playerInput.actions["Throttle"];
        brakeAction = playerInput.actions["Brake"];
        specialAction = playerInput.actions["Special"];

        spawnRocks = playerInput.actions["Spawn Rocks"];
    }

    private void Update()
    {
        // Vehicle Controls
        SteerInput = steerAction.ReadValue<float>();
        ThrottleInput = throttleAction.ReadValue<float>();
        BrakeInput = brakeAction.ReadValue<float>();
        SpecialInput = specialAction.ReadValue<float>();

        SpawnRockInput = spawnRocks.ReadValue<float>();
    }
}
