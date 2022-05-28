using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDetector : MonoBehaviour
{
    [Header("Vehicle Snapping")]
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private float anchorTime = 3f;

    [Header("Item Spawning")]
    [SerializeField] private float delayTime = 1f;
    private float startTime;

    private bool vehiclePresent = false;
    private ItemSpawner spawner;

    private void Start()
    {
        spawner = gameObject.GetComponentInChildren<ItemSpawner>();
        startTime = 0;
    }

    private void Update()
    {
        if (spawner == null)
            return;
        
        if (startTime == 0 && vehiclePresent)
        {
            startTime = Time.time + delayTime;
        }
        if (Time.time > startTime)
            spawner.spawnEnabled = vehiclePresent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(vehicle.transform))
        {
            vehiclePresent = true;

            StartCoroutine(AnchorToTarget());

            Rigidbody rb = vehicle.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }

        if (other.name.Contains("Clone"))
            other.gameObject.SetActive(false);       
    }

    private void OnTriggerExit(Collider other)
    {
        vehiclePresent = false;
        startTime = 0;
    }

    IEnumerator AnchorToTarget()
    {
        Vector3 originalPosition = vehicle.transform.position;
        Quaternion originalRotation = vehicle.transform.rotation;

        Quaternion tgtRot = Quaternion.Euler(targetRotation);

        float percent = 0f;

        while (percent <= 1)
        {
            vehicle.transform.position = Vector3.Lerp(originalPosition, targetPosition, percent);
            vehicle.transform.rotation = Quaternion.Lerp(originalRotation, tgtRot, percent);
            percent += Time.deltaTime * anchorTime;
            yield return null;
        }
    }
}
