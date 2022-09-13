using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedProjectile : MonoBehaviour
{
    public GameObject deathEffect;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (player != null)
            {
                player.Damage(1);
                Instantiate(deathEffect, this.transform.position, Quaternion.identity);
                DestroyThis(transform.parent.gameObject);
            }
        }

        if (other.TryGetComponent<GPlayer>(out var gPlayer))
        {
            if (gPlayer != null)
            {
                gPlayer.Damage(1);
                Instantiate(deathEffect, this.transform.position, Quaternion.identity);
                DestroyThis(transform.parent.gameObject);
            }
        }
    }

    public void DestroyThis(GameObject thingToDestroy) 
    {
        Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(thingToDestroy);
    }
}
