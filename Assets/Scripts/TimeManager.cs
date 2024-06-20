using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Light sun;

    [SerializeField, Range(0, 24)] private float timeofDay;

    [SerializeField] private float sunRotationSpeed;

    [Header("LightingPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;


    private void Update()
    {
        timeofDay += Time.deltaTime * sunRotationSpeed;
        if(timeofDay > 24)
        {
            timeofDay = 0;

        }
        UpdateRotation();
        UpdateLighting();
    }
    private void OnValidate()
    {
        UpdateRotation();
        UpdateLighting();
    }

    private void UpdateRotation()
    {
        float SunRotation = Mathf.Lerp(-90, 270, timeofDay / 24);
        sun.transform.rotation = Quaternion.Euler(SunRotation, sun.transform.rotation.y, sun.transform.rotation.z);

    }


    private void UpdateLighting()
    {
        float timeFraction = timeofDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);

    }


}
