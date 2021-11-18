using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBird : Enemy, IDamageable
{
    Rigidbody rb;

    public float bombTime, btSet;

    public Transform bombSalvo;

    public GameObject projectile;

    public GameObject deathEffect;

    public int health
    {
        get;
        set;
    }

    public override void Init()
    {
        base.Init();
        bombTime = btSet;
        rb = GetComponent<Rigidbody>();
        health = base._health;
        _currentTarget = _pointA.position;
    }

    public override void Movement()
    {
        base.Movement();

        if (bombTime > 0) 
        {
            bombTime -= Time.deltaTime;

            if (bombTime <= 0) 
            {
                bombTime = btSet;

                BombsAway();
            }
        }
    }
    public void Damage(int amount)
    {
        if (health < 1)
        {
            _anim.SetTrigger("Death");
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            _isDead = true;
        }
        else
        {
            health--;
            _isHit = true;
            _anim.SetTrigger("Hit");
            _anim.SetBool("InCombat", true);
            UIManager _UIManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
            _UIManager.Notification(this.name + " was damaged!");
        }
    }

    void BombsAway() 
    {
        GameObject bomb = Instantiate(projectile, bombSalvo.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            Damage(1);

            Player player = other.GetComponentInParent<Player>();

            if (player != null) 
            {
                player.EnemyJump();
            }
        }

        if (other.tag == "Ground") 
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
