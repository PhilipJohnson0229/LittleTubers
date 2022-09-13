using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOP : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public bool unlocked;

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)) 
        {
            if (player != null) 
            {
                anim.SetBool("Activate", true);

                if (unlocked)
                {
                    anim.SetBool("Unlocked", true);
                }
                else 
                {
                    anim.SetBool("Unlocked", false);
                }
            }
        }
    }

}
