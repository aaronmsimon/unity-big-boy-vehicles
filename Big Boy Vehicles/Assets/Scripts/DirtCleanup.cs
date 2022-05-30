using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCleanup : MonoBehaviour
{
    private StreetSweeperController streetSweeper;

    private void Start()
    {
        streetSweeper = GetComponentInParent<StreetSweeperController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (streetSweeper == null)
            return;
        if (other.CompareTag("Dirt") && streetSweeper.fansEnabled)
        {
            other.gameObject.SetActive(false);
        }
    }
}
