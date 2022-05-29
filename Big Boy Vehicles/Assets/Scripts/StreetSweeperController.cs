using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool armsMoving;

    private bool fansEnabled;
    private float fanCheckFreq = .5f;
    private float nextFanCheck;

    private void Start()
    {
        armsExtended = false;
        armsMoving = false;
        fansEnabled = false;
        nextFanCheck = Time.time + fanCheckFreq;
    }

    void Update()
    {
        if (GameManager.Instance.InputController.SecondaryInput == 1 && !armsMoving)
        {
            armsMoving = true;
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

        if (GameManager.Instance.InputController.SpecialInput == 1 && Time.time > nextFanCheck)
        {
            fansEnabled = !fansEnabled;
            nextFanCheck = Time.time + fanCheckFreq;
        }

        if (fansEnabled)
        {
            foreach (FanComplex fanComplex in fans)
            {
                foreach (Transform fan in fanComplex.fans)
                {
                    fan.Rotate(0, fanSpeed * Time.deltaTime, 0);
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
        armsMoving = false;
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
