using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 70f;
    [SerializeField] float addIntensity = 1f;


    FlashlightSystem flashlightSystem;

    private void Start() {
        GameObject.Find("Player").GetComponentInChildren<FlashlightSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            flashlightSystem.RestoreLightAngle(restoreAngle);
            flashlightSystem.AddLightIntensity(addIntensity);

            Destroy(gameObject);
        }
    }
}
