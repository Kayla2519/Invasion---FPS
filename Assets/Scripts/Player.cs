using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 0f;
    public int reserveAmmo = 50;
    public int maxReserveAmmo = 50;
    public int score = 0;
    public int winScore = 5000;
    public HealthBar healthBar;

    [Header("Sound Effects")]
    [SerializeField] AudioClip damageSound;
    private AudioSource audioSource;

    public Gun gun;

    public TMP_Text ammoText;
    public TMP_Text scoreText;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gun = GetComponentInChildren<Gun>();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth / maxHealth);
        UpdateScoreUI();
        ResetAmmo();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        PlayDamageSound();

        if (currentHealth < 0f) currentHealth = 0f;
        
        healthBar.SetHealth(currentHealth / maxHealth);

        if (currentHealth == 0f)
        {
            ResetAmmo();
            GoToScene("LoseScript");
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth / maxHealth);
    }

    public void UseAmmo(int amount)
    {
        if (gun.loadedAmmo < 0) gun.loadedAmmo = 0;
        UpdateAmmoUI();
    }

    public void PickUpAmmo(int amount)
    {
        reserveAmmo = Mathf.Min(reserveAmmo + amount, maxReserveAmmo);
        gun.reserveAmmo = reserveAmmo;

        UpdateAmmoUI();
    }

    public void ResetAmmo()
    {
        reserveAmmo = maxReserveAmmo;
        gun.loadedAmmo = gun.maxLoadedAmmo;
        gun.reserveAmmo = gun.maxReserveAmmo;

        UpdateAmmoUI();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + gun.loadedAmmo.ToString() + "/" + gun.reserveAmmo.ToString();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString() + "/2000";

        if (score >= winScore)
        {
            GoToScene("WinScript");
        }
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
}
