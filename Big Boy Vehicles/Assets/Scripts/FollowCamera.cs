using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 offsetPos;
    [SerializeField] private Vector2 offsetRot;
	[SerializeField] private bool mirrorTargetY;

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + target.rotation * offsetPos;
        transform.rotation = Quaternion.Euler(offsetRot.x, mirrorTargetY ? target.eulerAngles.y: 0, offsetRot.y);
    }
}