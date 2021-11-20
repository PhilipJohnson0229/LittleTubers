using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDamagePointPhase1 : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            if (BossPhase1Controller.instance.canBeHit == true)
            {
                BossPhase1Controller.instance.playerHurtMe = true;
                BossPhase1Controller.instance.DamageBoss();
                
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
