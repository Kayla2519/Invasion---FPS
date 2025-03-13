using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float healAmount = 10;
    public float fallSpeed = 5f;
    private bool isFalling = true;
    private bool hasLanded = false;
    private Vector3 fallVelocity;

    void Start()
    {
        fallVelocity = new Vector3(0, -fallSpeed, 0);
    }

    void Update()
    {
        if (isFalling && !hasLanded)
        {
            transform.position += fallVelocity * Time.deltaTime;

            if (transform.position.y <= 1.5)
            {
                hasLanded = true;
                StopFalling();
            }
        }
    }

    private void StopFalling()
    {
        fallVelocity = Vector3.zero;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (player.currentHealth != player.maxHealth)
            {
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
