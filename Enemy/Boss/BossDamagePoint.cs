using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDamagePoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            if (BossPhase2Controller.instance.canBeHit == true)
            {
                BossPhase2Controller.instance.DamageBoss();

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
