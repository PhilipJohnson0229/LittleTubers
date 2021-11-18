using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract lets us use abstract methods.
//I think it means you can op to not use parent class methods
public abstract class Enemy : MonoBehaviour
{
    public GameObject _coin;
    public int _coinsAmount;

    [SerializeField]
    protected int _health;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected int _gems;
    [SerializeField]
    protected Transform _pointA, _pointB;
    [SerializeField]
    protected Vector3 _currentTarget;
    [SerializeField]
    protected Animator _anim;
    [SerializeField]
    protected bool _isHit = false;
    [SerializeField]
    protected bool _isDead = false;
    [SerializeField]
    protected Player _player;
    public virtual void Init() 
    {
        _anim = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() ;
    }

    private void Start() 
    {
        Init();
       
    }

    public virtual void Attack()
    {
        Debug.Log("My name is " + this.gameObject.name);

    }

    public virtual void DestroyThis() 
    {
        Destroy(this.gameObject);
    }

    public virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false || _anim.GetBool("Kill") == true)
        {
            return;
        }

        if(_isDead == false) 
        {
            Movement();
        }
    }

    public virtual void Movement()
    {
        Vector3 _facing = transform.localEulerAngles;

        if (_currentTarget == _pointA.position)
        {   
            _facing.y = 0f;
            transform.localEulerAngles = _facing;
        }
        else if (_currentTarget == _pointB.position)
        { 
            _facing.y = 180f;
            transform.localEulerAngles = _facing;
        }

        if (transform.position == _pointA.position)
        {
            _currentTarget = _pointB.position;
            _anim.SetTrigger("Idle");
        }
        else if (transform.position == _pointB.position)
        {
            _currentTarget = _pointA.position;
            _anim.SetTrigger("Idle");
        }

        if(_isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        if (_player != null) 
        {
            float _distance = Vector3.Distance(this.transform.localPosition, _player.transform.localPosition);
            if (_distance > 5.0f)
            {
                _isHit = false;
                _anim.SetBool("InCombat", false);
            }

            Vector3 _direction = transform.localPosition - _player.transform.localPosition;

            if (_direction.z > 0 && _anim.GetBool("InCombat") == true)
            {
                _facing.y = 180f;
                transform.localEulerAngles = _facing;
            }
            else if (_direction.z < 0 && _anim.GetBool("InCombat") == true)
            {
                _facing.y = 0f;
                transform.localEulerAngles = _facing;
            }
        }

        
    }
    //an abstract methods constructor is an interface with a child class
    //basically the child class is required to call implement these methods
    //the child classes will simply implement this methoda dn fiil it with is own unique code
   
}
