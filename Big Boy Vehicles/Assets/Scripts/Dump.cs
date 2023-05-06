using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dump : MonoBehaviour
{
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private float smooth = 3f;

    private PlayerControls playerControls;
    private bool isDumping;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }   

    void Update()
    {
        // if (gameObject.transform.IsChildOf(GameManager.Instance.PlayerManager.activeVehicle.transform))
        //     isDumping = (GameManager.Instance.InputController.SpecialInput == 1);
        isDumping = playerControls.Driving.Special.ReadValue<float>() == 1;

        rotateDump();
    }

    void rotateDump()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAngle;
        if (isDumping)
        {
            tiltAngle = maxRotationAngle;
        } else
        {
            tiltAngle = transform.parent.eulerAngles.x;
        }

        // Rotate the dump by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAngle, transform.parent.eulerAngles.y, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
