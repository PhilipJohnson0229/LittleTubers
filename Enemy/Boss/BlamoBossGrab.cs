using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoBossGrab : MonoBehaviour
{
    public BossPhase1Controller blamo;
    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player") 
        {
            Player player = _other.GetComponent<Player>();
            if (player != null) 
            {
                player.BossEnsnare(blamo);
                blamo.anim.SetBool("PlayerCaught", true);
            }
        }
    }
   
}
