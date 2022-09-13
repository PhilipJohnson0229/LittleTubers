using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * 3f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            IDamageable _hit = other.GetComponent<IDamageable>();

            if (_hit != null)
            {
                _hit.Damage(1);
                Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        if (other.CompareTag("Ground")) 
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.tag == "KillerCar")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
