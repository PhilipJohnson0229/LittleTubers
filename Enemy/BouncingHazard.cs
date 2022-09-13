using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingHazard : MonoBehaviour
{
    public float speed;

    public float lifeSpan;

    public AnimatedProjectile core;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0) 
        {
            core.DestroyThis(gameObject);
        }
    }


    
}
