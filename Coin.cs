using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)) 
        {
            if (player != null) 
            {
                player.playerData.setCoins(player.playerData.getCoins() + 1);
                Destroy(this.gameObject);
            }
        }
    }
}
