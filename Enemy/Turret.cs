using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy, IDamageable
{
    public int health { get ; set; }

    [SerializeField]
    private GameObject _bullet;

    public override void Init()
    {
        base.Init();

        health = base._health;
    }

    public override void Movement()
    {
        Vector3 _direction = transform.localPosition - _player.transform.localPosition;
        Vector3 _facing = transform.localEulerAngles;

        if (_direction.z > 0)
        {
            _facing.y = 180f;
            transform.localEulerAngles = _facing;
        }
        else if (_direction.z < 0)
        {
            _facing.y = 0f;
            transform.localEulerAngles = _facing;
        }
    }
    public void Damage(int amount)
    {
        if (health < 1)
        {
            Vector3 _offset = new Vector3(0, 0.5f, 0);
            _anim.SetTrigger("Death");
            _isDead = true;
            //casting
            GameObject _coinDrop = Instantiate(_coin, this.transform.localPosition + _offset, Quaternion.identity) as GameObject;
            //here we would put code to handle assigning multiple coins if we want to go that route
            _coinDrop.GetComponent<Coin>()._amount = base._coinsAmount;
        }
        else
        {
            if (_isDead == true) { return; }
            health--;
            _isHit = true;
            _anim.SetTrigger("Hit");
            _anim.SetBool("InCombat", true);
            UIManager _UIManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
            _UIManager.Notification(this.name + " was damaged!");
        }
    }

    public override void DestroyThis()
    {
        Debug.Log("calling the override method");
        base.DestroyThis();
    }

    public override void Attack() 
    {
        Vector3 _offset = new Vector3(0, 0.5f, 0.5f);
        Vector3 _facing = transform.localEulerAngles;
        
        transform.localEulerAngles = _facing;
       
        Instantiate(_bullet, this.transform.position + _offset, Quaternion.Euler(_facing));
    }

    // Start is called before the first frame update
   
}
