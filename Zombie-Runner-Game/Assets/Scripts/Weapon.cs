using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Camera FPCamera;

    [Header("Ammo")]
    [SerializeField] Ammo ammoSlot; 
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;

    [Header("Weapon Features")]
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 100f;
    [SerializeField] float timeBetweenShoots = 0.2f;
    [SerializeField] bool ableHold = false; 

    [Header("Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    bool canShoot = true;

    private void OnEnable() {
        canShoot = true;
    }
    void Update()
    {
        GetFireInput();
        DisplayAmmo();
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString("00");
    }

    private void GetFireInput()
    {
        if (ableHold)
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
            if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                PlayMuzzleFlash();
                ProcessRaycast();
                ammoSlot.ReduceCurrentAmmo(ammoType);

                StartCoroutine(ShootDelay());
            }
            yield return new WaitForSeconds(timeBetweenShoots);
            canShoot = true;
        }
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) { return; }

            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }


    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impact, 0.1f);
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }
}
