using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFish : Enemy
{
    public override void Init()
    {
        base.Init();
      
        _currentTarget = _pointA.position;
      
    }

    public override void Movement()
    {
        Vector3 _facing = transform.localEulerAngles;

        if (_currentTarget == _pointA.position)
        {
            _facing.x = 90;
            playerModel.localEulerAngles = _facing;
        }
        else if (_currentTarget == _pointB.position)
        {
            _facing.x = -90;
            playerModel.localEulerAngles = _facing;
        }

        if (transform.position == _pointA.position)
        {
            _currentTarget = _pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == _pointB.position)
        {
            _currentTarget = _pointA.position;
            anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            other.TryGetComponent<Player>(out var player);

            if (player != null) 
            {
                anim.Play("Bite");
                player.Damage(2);
            }
        }
    }
}
