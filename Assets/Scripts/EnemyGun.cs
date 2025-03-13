using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] LayerMask hittableLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float weaponRange;

    [Header("Game Feel")]
    [SerializeField] GameObject bulletHole;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float bulletHolePositionOffset;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int bulletsPerShot;
    [SerializeField] float damage = 25f;
    [SerializeField] float nextFireTime = 0f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip gunshotSound;
    private AudioSource audioSource;

    Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            HandleMuzzleFlash();
            HandleRaycast();
            PlayGunshotSound();
        }
    }

    private void PlayGunshotSound()
    {
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }

    private void HandleRaycast()
    {
        RaycastHit hit;

        Vector3 origin = transform.position + transform.forward * 0.5f;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out hit, weaponRange, playerLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject == gameObject) return;

            if (((1 << hit.collider.gameObject.layer) & (hittableLayer | groundLayer | playerLayer)) != 0)
            {
                Instantiate(bulletHole, hit.point + (hit.normal * bulletHolePositionOffset), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0));
            }

            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                Player player = hit.collider.GetComponentInParent<Player>();

                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        }
    }

    //call this function from Shoot()
    private void HandleMuzzleFlash()
    {
        if (muzzleFlash.isPlaying)
            muzzleFlash.Stop();
        muzzleFlash.Play();
    }
}
