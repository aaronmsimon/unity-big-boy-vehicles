using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpController : MonoBehaviour
{
    [SerializeField] private float maxRotation = -10f;
    [SerializeField] private float smooth = 3f;

    public float Special { get; set; }

    private bool isDumping;

    void Update()
    {
        Special = GameManager.Instance.InputController.SpecialInput;

        if (Special == 1 && !isDumping)
        {
            isDumping = true;
        }

        if (Special == 0)
        {
            isDumping = false;
        }

        rotateDump();
    }

    void rotateDump()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAngle = maxRotation * (isDumping ? 1 : 0);

        // Rotate the dump by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAngle, transform.parent.eulerAngles.y, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
