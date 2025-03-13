using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammoAmount = 7;

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (player.reserveAmmo < player.maxReserveAmmo)
            {
                int ammoToAdd = Mathf.Min(player.maxReserveAmmo - player.reserveAmmo, ammoAmount);
                player.PickUpAmmo(ammoToAdd);

                player.UpdateAmmoUI();
                Destroy(gameObject);
            }
        }
    }
}
