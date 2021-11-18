using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCollider : MonoBehaviour
{
    public Berserker berserker;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            
            if (berserker != null)
            {
                berserker.Damage(1);

                if (berserker.ReturnPlayer() != null) 
                {
                    berserker.ReturnPlayer().Jump();
                }
            }
        }
    }
}
