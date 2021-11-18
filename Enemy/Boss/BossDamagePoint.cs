using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDamagePoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            if (BossController.instance.canBeHit == true)
            {
                BossController.instance.DamageBoss();

                Player player = other.gameObject.GetComponentInParent<Player>();

                player.EnemyJump();
            }
            else 
            {
                Player player = other.gameObject.GetComponentInParent<Player>();

                player.EnemyJump();
            }
            
            
        }

    }
}
