using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetHealth(float healthPercentage)
    {
        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider is not assigned in the Inspector!");
            return;
        }

        healthSlider.value = healthPercentage;
    }
}
