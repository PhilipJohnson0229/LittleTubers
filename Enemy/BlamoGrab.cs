using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoGrab : MonoBehaviour
{
    public Blamo blamo;

    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player")
        {
            Player player = _other.GetComponent<Player>();
            if (player != null)
            {
                player.Ensnare(blamo);
            }
        }

        if (_other.tag == "Enemy") 
        {
            Berserker humanScum = _other.GetComponent<Berserker>();

            if (humanScum != null) 
            {
                humanScum.Damage(4);
            }
        }
    }

}

