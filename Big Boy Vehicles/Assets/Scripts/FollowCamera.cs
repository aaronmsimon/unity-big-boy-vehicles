using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 offsetPos;
    [SerializeField] private Vector2 offsetRot;

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + target.rotation * offsetPos;
        transform.eulerAngles = new Vector3(offsetRot.x, target.eulerAngles.y, offsetRot.y);
    }
}