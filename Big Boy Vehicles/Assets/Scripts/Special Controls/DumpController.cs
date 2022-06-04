using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpController : MonoBehaviour
{
    [SerializeField] private float maxRotation = -10f;
    [SerializeField] private float smooth = 3f;

    private bool isDumping;

    void Update()
    {
        if (gameObject.transform.IsChildOf(GameManager.Instance.PlayerManager.activeVehicle.transform))
            isDumping = (GameManager.Instance.InputController.SpecialInput == 1);

        rotateDump();
    }

    void rotateDump()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAngle;
        if (isDumping)
        {
            tiltAngle = maxRotation;
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
