using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingHazard : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce;
    public float speed;

    public int health;

    public GameObject deathEffect;
    // Start is called before the first frame update


    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.Damage(1);
                health--;
            }
        }

        if (other.tag == "Ground") 
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            health--;

            if (health <= 0) 
            {
                Instantiate(deathEffect, this.transform.position, Quaternion.identity);
                health = 0;
                Destroy(this.gameObject, .3f); 
            }
        }
    }

    
}
