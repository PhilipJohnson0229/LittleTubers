using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int trampolineJumpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox") 
        {
            Player player = GameObject.FindObjectOfType<Player>();

            if (player != null)
            {
                player.TrampolineJump(trampolineJumpSound);
            }
        }
    }
}
