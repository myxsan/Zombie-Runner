using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensivity = 2f;
    [SerializeField] float zoomInSensivity = 0.5f;

    RigidbodyFirstPersonController fpsController;

    bool zoomedInToggle = false;

    private void Start() {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();    
    }

    private void Update() {
        if(Input.GetMouseButton(1))
        {
            if(zoomedInToggle == false)
            {
                zoomedInToggle = true;

                fpsCamera.fieldOfView = zoomedInFOV;
                fpsController.mouseLook.XSensitivity = zoomInSensivity;
                fpsController.mouseLook.YSensitivity = zoomInSensivity;
            }
        }
        else
        {
            if(zoomedInToggle == true)
            {
                zoomedInToggle = false;

                fpsCamera.fieldOfView = zoomedOutFOV;
                fpsController.mouseLook.XSensitivity = zoomOutSensivity;
                fpsController.mouseLook.YSensitivity = zoomOutSensivity;
            }
        }
    }

}
