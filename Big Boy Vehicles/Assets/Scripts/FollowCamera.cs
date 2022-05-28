using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 eulerRotation;
    //[SerializeField] private float damper;

    private void Start()
    {
        transform.eulerAngles = eulerRotation;
    }

    void Update()
    {
        if (target == null)
            return;

        transform.position = target.position + target.rotation * offset;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
    }
}
