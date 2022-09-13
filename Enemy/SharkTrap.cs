using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTrap : MonoBehaviour
{
    [SerializeField]
    bool withinRange;
    [SerializeField]
    Animator anim;

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
                withinRange = true;

                anim.SetBool("WithinRange", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (player != null)
            {
                withinRange = false;

                anim.SetBool("WithinRange", false);
            }
        }
    }

    public bool PlayerWithinRange() 
    {
        return withinRange;
    }

    public void ResetTrap() 
    {
        withinRange = false;
    }
}
