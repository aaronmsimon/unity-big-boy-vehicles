using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionLight;
    [SerializeField] private LightingPreset lightingPreset;
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private float secondsPerDay;

    private void Update()
    {
        if (lightingPreset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * 24 / secondsPerDay;
            timeOfDay %= 24; // Clamp between 0-24
            UpdateLighting(timeOfDay / 24f);
        } else
        {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightingPreset.FogColor.Evaluate(timePercent);

        if (directionLight != null)
        {
            directionLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
            directionLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));
        }
    }

    private void OnValidate()
    {
        if (directionLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            directionLight = RenderSettings.sun;
        } else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionLight = light;
                    return;
                }
            }
        }
    }
}
