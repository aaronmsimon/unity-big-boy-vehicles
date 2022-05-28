using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDetector : MonoBehaviour
{
    [Header("Vehicle Snapping")]
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float anchorTime = 3f;

    [Header("Item Spawning")]
    [SerializeField] private float delayTime = 1f;

    private bool vehiclePresent = false;
    private ItemSpawner spawner;

    private void Start()
    {
        spawner = gameObject.GetComponentInChildren<ItemSpawner>();
    }

    private void Update()
    {
        if (spawner != null)
            spawner.canSpawn = vehiclePresent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(vehicle.transform))
        {
            vehiclePresent = true;

            Rigidbody rb = vehicle.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            StartCoroutine(AnchorToTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        vehiclePresent = false;
    }

    IEnumerator AnchorToTarget()
    {
        Vector3 originalPosition = vehicle.transform.position;
        Quaternion originalRotation = vehicle.transform.rotation;

        float percent = 0f;

        while (percent <= 1)
        {
            vehicle.transform.position = Vector3.Lerp(originalPosition, targetPosition, percent);
            vehicle.transform.rotation = Quaternion.Lerp(originalRotation, Quaternion.identity, percent);
            percent += Time.deltaTime * anchorTime;
            yield return null;
        }
    }
}
