using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;

    public void SetHealth(float healthPercentage)
    {
        healthBarFill.fillAmount = healthPercentage;
    }
}
