using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoGroundCheck : MonoBehaviour
{
    public Blamo blamo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            blamo.SetGroundedState();
        } 
    }
}
