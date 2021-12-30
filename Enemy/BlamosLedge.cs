using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamosLedge : MonoBehaviour
{
    [SerializeField]
    private Vector3 _standPos, targetRotation;
    public Vector3 _targetPosition;

    public Blamo blamo;


    void OnTriggerEnter(Collider _other)
    {
        //_targetPosition = new Vector3(-0.02f, 73.3638f, 123.251f);

        if (_other.tag == "Ledge_Grab_Checker")
        {
            //try and grab the character controller
            blamo = _other.transform.parent.GetComponentInParent<Blamo>();

            if (blamo != null)
            {
                blamo.GrabLedge(_targetPosition, this);
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
