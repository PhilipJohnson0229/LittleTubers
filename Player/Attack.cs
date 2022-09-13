using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  

    public int amount;

    void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent<Player>(out var player)) 
        {
            if (player != null) 
            {
                player.Damage(amount);
            }
        }
    }
}
