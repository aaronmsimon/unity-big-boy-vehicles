using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StreetSweeperController : MonoBehaviour
{
    [System.Serializable]
    public class FanComplex
    {
        public Transform fanArm;
        public Transform[] fans;
    }

    [SerializeField] private List<FanComplex> fans;
    [SerializeField] private float armExtensionRatio = 1.5f;
    [SerializeField] private float armMoveTime = 3f;
    [SerializeField] private float fanSpeed;
    
    private bool armsExtended;
    public bool fansEnabled { get; private set; }

    private void Start()
    {
        armsExtended = false;

        GameManager.Instance.InputController.playerInput.actions["Special"].performed += SpecialAction;
        GameManager.Instance.InputController.playerInput.actions["Secondary"].performed += SecondaryAction;
    }

    private void Update()
    {
        if (fansEnabled)
        {
            foreach (FanComplex fanComplex in fans)
            {
                foreach (Transform fan in fanComplex.fans)
                {
                    fan.Rotate(0, fanSpeed * Time.deltaTime * fan.localScale.x * (fan.localPosition.z > 0 ? -1 : 1), 0);
                }
            }
        }
    }

    private void SpecialAction(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.PlayerManager.activeVehicle == gameObject)
            fansEnabled = !fansEnabled;
    }

    private void SecondaryAction(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.PlayerManager.activeVehicle == gameObject)
        {
            armsExtended = !armsExtended;

            foreach (FanComplex fanComplex in fans)
            {
                StartCoroutine(AdjustFanArms(fanComplex));
                foreach (Transform fan in fanComplex.fans)
                {
                    StartCoroutine(AdjustFans(fan));
                }
            }
        }
    }

    IEnumerator AdjustFanArms(FanComplex fanComplex)
    {
        Vector3 originalScale = fanComplex.fanArm.localScale;
        Vector3 newScale = new Vector3((armsExtended ? armExtensionRatio : 1), 1, 1);

        float percent = 0f;

        while (percent <= 1)
        {
            fanComplex.fanArm.localScale = Vector3.Lerp(originalScale, newScale, percent);
            percent += Time.deltaTime * armMoveTime;
            yield return null;
        }
    }

    IEnumerator AdjustFans(Transform fan)
    {
        Vector3 originalPos = fan.localPosition;
        Vector3 newPos = new Vector3((armsExtended ? fan.localPosition.x * armExtensionRatio : fan.localPosition.x / armExtensionRatio), fan.localPosition.y, fan.localPosition.z);

        float percent = 0f;

        while (percent <= 1)
        {
            fan.localPosition = Vector3.Lerp(originalPos, newPos, percent);
            percent += Time.deltaTime * armMoveTime;
            yield return null;
        }
    }
}
