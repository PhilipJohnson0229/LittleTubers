using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject shortoutEffect;
    private void OnTriggerStay(Collider other) 
    {
        Rigidbody _box = other.GetComponent<Rigidbody>();
        if (_box != null)
        {
            _box.isKinematic = true;
            GiantBird.birdDefeated += TurnOnShortOutEffect;
        }

        MeshRenderer _mr = other.GetComponent<MeshRenderer>();
        if (_mr != null)
        {
            _mr.material.color = Color.blue;
        }

    }

    void TurnOnShortOutEffect() 
    {
        shortoutEffect.SetActive(true);
    }
}
