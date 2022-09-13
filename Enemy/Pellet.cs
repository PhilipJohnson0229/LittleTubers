using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable _hit = other.GetComponent<IDamageable>();

            if (_hit != null)
            {
                _hit.Damage(1);
                Destroy(this.gameObject);
            }
        }
    }
}
