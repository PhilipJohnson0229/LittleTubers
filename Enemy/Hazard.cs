using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public GameObject deathEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage(2);
            }
        }

        if (other.tag == "KillerCar")
        {
            deathEffect.SetActive(true);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
