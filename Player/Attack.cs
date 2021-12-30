using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  

    public int amount;

    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player") 
        {
            IDamageable hit = _other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage(amount);             
            }
        }
    }
}
