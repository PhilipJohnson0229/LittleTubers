using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBird : Enemy, IDamageable
{
    Rigidbody rb;

    public float bombTime, btSet;

    public Transform bombSalvo;

    public GameObject projectile;

    public bool shootsProjectile = true;

    public GameObject deathEffect, custscene;

    public EnemySpawnPoint esPoint;

    public delegate void onDefeated();
    public static onDefeated birdDefeated;

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
      
        
        birdDefeated += ActivateCutscene;
    }

    public override void Movement()
    {
        base.Movement();

        if (shootsProjectile) 
        {
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
    }

    public void Damage(int amount)
    {
        health--;
        
        if (health < 1)
        {
            _isDead = true;
            Kill();
        }
        else
        {
            _isHit = true;
            anim.SetTrigger("Hit");
            anim.SetBool("InCombat", true);
            
        }
    }

    void BombsAway()
    {
        GameObject bomb = Instantiate(projectile, bombSalvo.position, bombSalvo.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            if (!player.playerData.isInHell()) 
            {
                Damage(1);
            }
            

            player.EnemyJump();
           
        }

        if (other.tag == "Ground")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        if (other.tag == "Moving Box")
        {

            custscene.SetActive(true);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            birdDefeated();
            
        }
    }

    public void Kill() 
    {
        if (anim != null) 
        {
            anim.SetTrigger("Death");
        }
        
        
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;

        if (esPoint != null) 
        {
            esPoint.VerifyChain();
        }
    }

    public void ActivateCutscene() 
    {
        Debug.Log("The bird was killed");
        if (!custscene.activeInHierarchy && custscene!= null) 
        {
            custscene.SetActive(true);
        }
    }

    public override void SetPatrolPoints(Transform a, Transform b)
    {
        base.SetPatrolPoints(a, b);
    }

    public override void Revive() 
    {
        _isDead = false;
    }

    public override bool IsDead()
    {
        Debug.Log(_isDead);
        return _isDead;
    }

    private void OnDisable()
    {

        birdDefeated -= ActivateCutscene;
    }
  
}
