using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] Ammo ammoSlot; 

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    [SerializeField] float range = 100f;
    [SerializeField] float damage = 100f;
    [SerializeField] float timeBetweenShoots = 0.2f;
    [SerializeField] bool ableHold = false; 

    bool canShoot = true;

    private void OnEnable() {
        canShoot = true;
    }
    void Update()
    {
        if(ableHold)
        {
            if(Input.GetMouseButton(0))
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
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
            if(ammoSlot.GetCurrentAmmo() > 0)
            {
                PlayMuzzleFlash();
                ProcessRaycast();
                ammoSlot.ReduceCurrentAmmo();

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
