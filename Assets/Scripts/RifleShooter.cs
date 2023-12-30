using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooter : MonoBehaviour
{
    public float fireRate = 60f; //bullets per minute

    public Transform fireTransform;
    public GameObject onHitEffect;
    public GameObject muzzleFlashEffect;

    [Header("Recoil")]
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 18f;

    public Vector3 recoilKickback = new Vector3(0.015f, 0, -0.2f);
    public Vector3 recoilRotation = new Vector3(10f, 5f, 7f);

    private Transform mainCamera;

    private float fireDelay;
    private float nextFireTime;

    //these are both local
    private Vector3 currentRecoilPosition;
    private Quaternion currentRecoilRotation;

    private Vector3 positionalStartingOffset;
    private Quaternion rotationalStartingOffset;

    private void Start()
    {
        positionalStartingOffset = transform.localPosition;
        rotationalStartingOffset = transform.localRotation;

        currentRecoilPosition = positionalStartingOffset;
        currentRecoilRotation = rotationalStartingOffset;

        mainCamera = Camera.main.transform;

        fireDelay = 60f / fireRate;
    }

    private void Update()
    {      
        //resetting force
        currentRecoilPosition = Vector3.Lerp(currentRecoilPosition, positionalStartingOffset, positionalReturnSpeed * Time.deltaTime);
        currentRecoilRotation = Quaternion.Lerp(currentRecoilRotation, rotationalStartingOffset, rotationalReturnSpeed * Time.deltaTime);

        transform.localPosition = Vector3.Slerp(transform.localPosition, currentRecoilPosition, positionalRecoilSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, currentRecoilRotation, rotationalRecoilSpeed * Time.deltaTime);
    }

    public void Fire()
    {
        if (!CanFire())
        {
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

        //do a recoil
        currentRecoilPosition = new Vector3(
            Random.Range(-recoilKickback.x, recoilKickback.x),
            Random.Range(-recoilKickback.y, recoilKickback.y),
            Random.Range(recoilKickback.z, recoilKickback.z));

        currentRecoilRotation = Quaternion.Euler(
            Random.Range(recoilRotation.x, recoilRotation.x),
            Random.Range(-recoilRotation.y, recoilRotation.y),
            Random.Range(-recoilRotation.z, recoilRotation.z));

        currentRecoilPosition += positionalStartingOffset;
        currentRecoilRotation = rotationalStartingOffset * currentRecoilRotation;
    }

    private bool CanFire()
    {
        return Time.time >= nextFireTime;
    }


}
