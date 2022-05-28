using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveConstraints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.constraints = RigidbodyConstraints.None;
    }
}
