using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : Enemy, IDamageable
{
    // Start is called before the first frame update
    public bool playerIsCloseEnough;

    public bool wakingUp = true;

    public Vector3 _direction;

    public Transform playerModel;

    public float h;

    private Rigidbody rb;

    public Material playerMat;

    public Color playerColor;
    public int health { get; set; }

    public int useThisHealth;
    public override void Init()
    {
        base.Init();
        _isDead = false;
        rb = GetComponent<Rigidbody>();
        playerColor = playerMat.color;
    }

    // Update is called once per frame
    public override void Update()
    {
        _direction = transform.localPosition - _player.transform.localPosition;

        if (Mathf.Abs(_direction.z) < 7)
        {
            playerIsCloseEnough = true;
            wakingUp = false;
            _anim.SetBool("Wake", false);
        }
        else { playerIsCloseEnough = false; }


        if (_player != null && playerIsCloseEnough && !_isDead && !wakingUp &&_anim.GetBool("canMove"))
        {
            Movement();
            _anim.SetBool("Moving", true);
        }
        else 
        {
            _anim.SetBool("Moving", false);
            return;
        }
    }

    public override void Movement()
    {

        Vector3 _facing = transform.eulerAngles;
        Vector3 velocity = new Vector3(0, 0, h) * _speed;


        rb.AddForce(velocity * Time.deltaTime);
       

        if (_direction.z < 0)
        {
            _facing.y = 0;
            playerModel.localEulerAngles = _facing;
            h = 1;
        } else if (_direction.z > 0)
        {
            _facing.y = 180f;
            playerModel.localEulerAngles = _facing;
            h = -1;
        }

        
    }

    public void Damage(int amount)
    {
        if (!_isDead) 
        {
            useThisHealth--;

            StartCoroutine(Blink(1.5f));

            if (useThisHealth <= 0)
            {
                useThisHealth = 0;
                _isDead = true;
                _anim.SetBool("Dead", true);

                Destroy(this.gameObject, 3f);
            }
        }
    }

    
    IEnumerator Blink(float timer)
    {
        bool blinking = false;
        float blinkingTimer = timer;

        while (blinkingTimer > 0)
        {
            blinkingTimer -= .1f;

            blinking = !blinking;

            if (blinking)
            {
                playerMat.color = Color.red;
            }
            else
            {
                playerMat.color = playerColor;
            }

            yield return new WaitForSeconds(.05f);
        }

        
        playerMat.color = playerColor;
        blinking = false;
    }

    public Player ReturnPlayer() 
    {
        return _player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isDead) 
        {
            if (_player != null) 
            {
                _player.Damage(1);
            }
        }
    }
}

