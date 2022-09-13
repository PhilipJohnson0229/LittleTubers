using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPosition, _standPos, targetRotation;

    public Player player;

 
    void OnTriggerEnter(Collider _other)
    {
        //_targetPosition = new Vector3(-0.02f, 73.3638f, 123.251f);
       
        if (_other.tag == "Ledge_Grab_Checker")
        {
            
            //try and grab the character controller
            player = _other.transform.parent.GetComponentInParent<Player>();
           
            if (player != null)
            {
                player.GrabLedge(_targetPosition, this);
            }
          
        }
    }

    public Vector3 GetStandPos() 
    {
        return _standPos;
    }

    public Vector3 GetLedgeDirection()
    {
        return targetRotation;
    }
}
