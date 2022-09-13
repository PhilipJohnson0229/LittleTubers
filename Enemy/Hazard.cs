using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public GameObject deathEffect;

    public int amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage(amount);
            }
        }

        if (other.tag == "HellReaper") 
        {
            Blamo blamo = other.GetComponent<Blamo>();
            //Debug.Log("Hes dead");
            if(blamo != null)
            {
                blamo.Defeat();
            }
        }

        if (other.tag == "KillerCar")
        {
            deathEffect.SetActive(true);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "BirdKey")
        {
            if (other.TryGetComponent<GiantBird>(out var enemy)) 
            {
                if (enemy != null) 
                {
                    enemy.Kill();
                }
            }
        }
    }
}
