using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] LayerMask hittableLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float weaponRange;

    [Header("Game Feel")]
    [SerializeField] GameObject bulletHole;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float bulletHolePositionOffset;
    [SerializeField] float fireRate;
    [SerializeField] int bulletsPerShot;
    [SerializeField] float damage = 25f;
    bool canShoot = true;
    float thresholdTime;

    [Header("Reloading")]
    public int maxReserveAmmo = 50;
    public int maxLoadedAmmo = 10;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] bool isReloading = false;

    [Header("Ammo")]
    public int loadedAmmo;
    public int reserveAmmo;

    [Header("Sound Effects")]
    [SerializeField] AudioClip gunshotSound;
    private AudioSource audioSource;

    [SerializeField] TextMeshProUGUI reloadingText;
    [SerializeField] Image backgroundImage;
    
    Player player;
    Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        reloadingText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (thresholdTime < Time.time && loadedAmmo > 0 && !isReloading)
        {
            canShoot = true;
            thresholdTime = Time.time + fireRate;
        }
        else
        {
            canShoot = false;
        }

        if (Input.GetMouseButton(0) && canShoot && !isReloading && loadedAmmo > 0)
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && reserveAmmo > 0 && loadedAmmo < maxLoadedAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        HandleMuzzleFlash();
        HandleRaycast();
        PlayGunshotSound();

        loadedAmmo -= bulletsPerShot;

        if (player != null)
        {
            player.UseAmmo(bulletsPerShot);
        }
    }

    private void HandleRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, weaponRange))
        {
            if (((1 << hit.collider.gameObject.layer) & hittableLayer) != 0)
            {
                Instantiate(bulletHole, hit.point + (hit.normal * bulletHolePositionOffset), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0));
            }
            
            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                Instantiate(bulletHole, hit.point + (hit.normal * bulletHolePositionOffset), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0));
            }

            if (((1 << hit.collider.gameObject.layer) & enemyLayer) != 0)
            {
                Instantiate(bulletHole, hit.point + (hit.normal * bulletHolePositionOffset), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0));

                EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
                
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
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

    private void PlayGunshotSound()
    {
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        reloadingText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);

        // Reload gun
        int ammoNeeded = maxLoadedAmmo - loadedAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);

        // Wait for reload time
        yield return new WaitForSeconds(reloadTime);

        // Refill ammo to in gun and update reserve ammo
        loadedAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;

        reserveAmmo = Mathf.Clamp(reserveAmmo, 0, maxReserveAmmo);

        player.reserveAmmo = reserveAmmo;
        player.UpdateAmmoUI();

        isReloading = false;

        reloadingText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }
}



// Source: https://medium.com/eincode/the-simplest-way-to-create-a-first-person-shooter-ebc408264aea //
