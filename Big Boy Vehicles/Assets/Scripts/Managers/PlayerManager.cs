using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject vehicleRoot;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private GameObject vehicleName;

    private Transform[] vehicles;
    private int vehicleIndex;
    [HideInInspector] public GameObject activeVehicle;

    private FollowCamera followMainCamera;
    private FollowCamera followMinimapCamera;

    private void Awake()
    {
        vehicles = new Transform[vehicleRoot.transform.childCount];
        for (int i = 0; i < vehicleRoot.transform.childCount; i++)
        {
            vehicles[i] = vehicleRoot.transform.GetChild(i);
        }
        vehicleIndex = 0;

        followMainCamera = mainCamera.GetComponent<FollowCamera>();
        followMinimapCamera = minimapCamera.GetComponent<FollowCamera>();

        SetupVehicle();
    }

    private void Start()
    {
        GameManager.Instance.InputController.playerInput.actions["Switch"].performed += SwitchVehicle;
    }

    private void SwitchVehicle(InputAction.CallbackContext context)
    {
        if (vehicleIndex < vehicles.Length - 1)
        {
            vehicleIndex++;
        } else
        {
            vehicleIndex = 0;
        }

        SetupVehicle();
    }

    private void SetupVehicle()
    {
        activeVehicle = vehicles[vehicleIndex].gameObject;

        followMainCamera.target = vehicles[vehicleIndex];
        followMinimapCamera.target = vehicles[vehicleIndex];

        vehicleName.GetComponent<Text>().text = vehicles[vehicleIndex].name;
    }

}
