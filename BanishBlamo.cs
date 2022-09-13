using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanishBlamo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Blamo>(out var blamo)) 
        {
            if (blamo != null) 
            {
                blamo.Banish();
            }
        }
    }
}

