using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGround : MonoBehaviour
{
    [SerializeField]
    private bool _grounded;
    [SerializeField]
    private float _distance = 0.3f;
   
    // Update is called once per frame
    public void GroundCheck()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, _distance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("we found the ground");
            _grounded = true;
        }
        else 
        {
            _grounded = false;
        } 
    }

    public bool IsGrounded()
    {
        GroundCheck();
        return _grounded;
    }

    public void ResetGrounded() 
    {
        _grounded = false;
    }
}
