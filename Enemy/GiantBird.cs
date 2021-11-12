using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBird : Enemy, IDamageable
{
    public int health
    {
        get;
        set;
    }

    public override void Init()
    {
        base.Init();
       
        health = base._health;
        _currentTarget = _pointA.position;
    }

    public override void Movement()
    {
        base.Movement();        
    }
    public void Damage(int amount)
    {
        if (health < 1)
        {
            _anim.SetTrigger("Death");
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

    
    //if we decide to create jump funcitonality we will use raycast and ignore all layers except for the ground
    //we will gump with rigidbody.velocity = new vector3() and try to apply gravity
    //at the end of this line we use a bit shift to only detect layer 8 because a layer mask is a 32 bit array.
    //basically we are placing a 1 on the 8th layer of a 32bit integer array of 1's and 0's
    //if we did not use bit shift operator we create a layermask variable and call it with layermask.value instead of 1 << 8
    //Raycast _hitInfo = Physics.Raycast(transform.position, Vector3.down, 1.0f, 1 << 8 )
    //Debug.DrawRay(transform.position, Vector3.down, color.green)
}
