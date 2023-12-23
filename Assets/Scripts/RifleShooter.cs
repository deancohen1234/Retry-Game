using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooter : MonoBehaviour
{
    public float fireRate = 60f; //bullets per minute

    public Transform fireTransform;
    public GameObject onHitEffect;
    public GameObject muzzleFlashEffect;

    private Transform mainCamera;

    private float fireDelay;
    private float nextFireTime;

    private void Start()
    {
        mainCamera = Camera.main.transform;

        fireDelay = 60f / fireRate;
    }

    public void Fire()
    {
        if (!CanFire())
        {
            Debug.Log("No firing");
            return;
        }

        nextFireTime = Time.time + fireDelay;

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 80f))
        {
            //put effect on it
            GameObject hitEffect = Instantiate(onHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffect, 2f);
        }
    }

    private bool CanFire()
    {
        return Time.time >= nextFireTime;
    }
}
