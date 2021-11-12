using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        float _distance = Vector3.Distance(transform.position, other.transform.position);

        if (_distance < 0.05f) 
        {
            //caching components into local varibales is just good practice
            Rigidbody _box = other.GetComponent<Rigidbody>();
            if (_box != null)
            {
                _box.isKinematic = true;
            }

            MeshRenderer _mr = other.GetComponent<MeshRenderer>();
            if (_mr != null)
            {
                _mr.material.color = Color.blue;
            }
            
            Destroy(this);
        }
    }
}
