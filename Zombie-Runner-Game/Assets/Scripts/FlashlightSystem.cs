using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;

    float defaultLightIntensity;

    Light flashlight;

    private void Start() 
    {
        flashlight = GetComponent<Light>();

        defaultLightIntensity = flashlight.intensity;
    }

    private void Update() {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        flashlight.spotAngle = restoreAngle;
    }

    public void AddLightIntensity(float intensityAmount)
    {
        if(flashlight.intensity <= defaultLightIntensity)
        {
            flashlight.intensity += intensityAmount;
        }
    }

    private void DecreaseLightIntensity()
    {
        if(flashlight.intensity >= 0)
        {
            flashlight.intensity -= lightDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightAngle()
    {
        if(flashlight.spotAngle >= minimumAngle)
        {
            flashlight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }
}
