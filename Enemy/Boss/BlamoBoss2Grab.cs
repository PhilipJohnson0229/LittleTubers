using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoBoss2Grab : MonoBehaviour
{
    public BossPhase2Controller blamo;

    public bool isLeftGrab = false;
    void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player") 
        {
            Player player = _other.GetComponent<Player>();
            if (player != null) 
            {
                player.Boss2Ensnare(blamo);
                blamo.anim.SetBool("PlayerCaught", true);

                if (isLeftGrab) 
                {
                    blamo.anim.SetTrigger("leftHandGrab");
                   
                }
                else 
                {
                    blamo.anim.SetTrigger("rightHandGrab");
                    
                }
            }
        }

        if (_other.tag == "Left Wall" || _other.tag == "Right Wall") 
        {
           
            Instantiate(blamo.destroyWallsPrefab, _other.transform.position, Quaternion.identity);

            Destroy(_other.gameObject);
        }
    }
   
}
