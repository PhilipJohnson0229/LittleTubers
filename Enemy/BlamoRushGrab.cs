using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoRushGrab : MonoBehaviour
{
    public RushingBlamo blamo;

    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player")
        {
            Player player = _other.GetComponent<Player>();
            if (player != null)
            {
                player.RushEnsnare(blamo);
            }
        }

        if (_other.tag == "InTheWay")
        {
            Animator anim = _other.GetComponent<Animator>();

            if (anim != null) 
            {
                anim.SetBool("Activate", true);
            }
        }
    }
}