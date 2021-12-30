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

    public GameObject ragdoll, hurtbox;

    private float h;

    private Rigidbody rb;

    public Material playerMat;

    private Color playerColor;


    public int health { get; set; }

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
            if (Mathf.Abs(_direction.z) < 1)
            {
                _anim.SetBool("Attacking", true);

            }
            else if (Mathf.Abs(_direction.z) > 1)
            {

                _anim.SetBool("Attacking", false);
            }

        }
        else
        {
            _anim.SetBool("Attacking", false);
            playerIsCloseEnough = false;
        }


        if (_player != null && !IsDead() && !wakingUp && CanMove() && !IsHurt())
        {
            Movement();
            _anim.SetBool("Moving", true);
        }
        else
        {
            _anim.SetBool("Moving", false);
            return;
        }

        if (IsAntiAir() || IsHurt() || IsDead())
        {
            hurtbox.SetActive(false);
        }
        else { hurtbox.SetActive(true); }
    }

    public override void Movement()
    {

        Vector3 _facing = transform.eulerAngles;
        Vector3 velocity = new Vector3(h, 0, 0) * _speed;

        if (!IsAttacking()) 
        {
           transform.Translate(velocity * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (!_isDead) 
        {
            if (_direction.z < 0)
            {
                _facing.y = -60f;
                playerModel.localEulerAngles = _facing;
                h = -1;
            }
            else if (_direction.z > 0)
            {
                _facing.y = 60f;
                playerModel.localEulerAngles = _facing;
                h = 1;
            }
        }
    }

    public void Damage(int amount)
    {
        if (!WakingUp() && !IsHurt()) 
        {
            if (!IsDead())
            {
              
                _health--;
                _anim.SetTrigger("Hurt");

                StartCoroutine(Blink(1.5f));

                if (_health <= 0)
                {
                    _health = 0;
                    _isDead = true;
                    
                    Quaternion lastRotation = playerModel.rotation;
                    playerModel.gameObject.SetActive(false);

                    GameObject deathEffect = Instantiate(ragdoll, transform.position, lastRotation);
                    if (_coin != null) 
                    {
                        GameObject droppedItem = Instantiate(_coin, this.transform.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                    }
   
                    deathEffect.transform.parent = this.transform;
                    Destroy(gameObject, 5f);
                    
                }
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

    public bool IsDead() 
    {
        return _isDead;
    }

    public bool WakingUp()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Activate");
    }

    public bool IsHurt() 
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
    }

    public bool IsAttacking() 
    {
        return _anim.GetBool("Attacking");
    }

    public bool CanMove()
    {
        return _anim.GetBool("canMove");
    }

    public bool IsAntiAir() 
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("AntiAir");
    }
}

