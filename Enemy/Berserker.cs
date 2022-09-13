using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : Enemy, IDamageable
{
    [SerializeField]
    private float maxDistance = 7;

    [SerializeField]
    private float talkTimer = 1f;

    [SerializeField]
    private int talkInt, damageSFX;

    [SerializeField]
    private Transform currentTarget;

    private bool wakingUp = true, isRespawnable = false, playerIsCloseEnough;

    private Vector3 direction;

    private float h;

    private Rigidbody rb;

    private Color playerColor;

    public Material playerMat;   

    public Transform activator;

    public GameObject ragdoll, hurtbox;

    public int health { get; set; }

    public delegate void onKilled();
    public static onKilled scumKilled;

    public override void Init()
    {
        base.Init();
        _isDead = false;
        rb = GetComponent<Rigidbody>();
        playerColor = playerMat.color;

        currentTarget = player.transform;

        scumKilled += Kill;
    }

    // Update is called once per frame
    public override void Update()
    {
        direction = transform.localPosition - currentTarget.localPosition;

        if (Mathf.Abs(direction.z) < maxDistance)
        {
            
            playerIsCloseEnough = true;
            wakingUp = false;
            anim.SetBool("Wake", false);
            if (Mathf.Abs(direction.z) < 1)
            {
                if (currentTarget == player.transform)
                {
                    anim.SetBool("Attacking", true);
                }
                else 
                {
                    playerIsCloseEnough = false;
                }
                

            }
            else if (Mathf.Abs(direction.z) > 1)
            {

                anim.SetBool("Attacking", false);
            }

        }
        else
        {
            anim.SetBool("Attacking", false);
            playerIsCloseEnough = false;
        }


        if (player != null && !IsDead() && !wakingUp && CanMove() && !IsHurt() && playerIsCloseEnough)
        {
            Movement();
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
            return;
        }

        if (IsAntiAir() || IsHurt() || IsDead())
        {
            hurtbox.SetActive(false);
        }
        else 
        {
            if (hurtbox != null) 
            {
                hurtbox.SetActive(true);
            }
        }

        if (talkTimer > 0) 
        {
            talkTimer -= Time.deltaTime;

            if (talkTimer <= 0) 
            {
                talkInt = Random.Range(23, 27);
                talkTimer = Random.Range(3.0f, 7.0f);
                if (!player.playerData.isInHell()) 
                {
                    speak(talkInt);
                }
            }
        }

        if (!player.isActiveAndEnabled || !playerIsCloseEnough) 
        {
            SendBackHome();
        }
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
            if (direction.z < 0)
            {
                _facing.y = -60f;
                playerModel.localEulerAngles = _facing;
                h = -1;
            }
            else if (direction.z > 0)
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

                anim.SetTrigger("Hurt");

                AudioManager.instance.PlaySoundEffects(damageSFX);

                StartCoroutine(Blink(1.5f));

                if (_health <= 0)
                {
                    _health = 0;

                    if (isRespawnable)
                    {
                        scumKilled();
                    }
                    else 
                    {
                        Kill();
                    }
                    
                }
            }
        }
    }

    public void speak(int animNum)
    {
        if (anim.GetBool("MouthOpen") == false)
        {
            anim.SetBool("MouthOpen", true);
            anim.SetInteger("FaceAnim", animNum);

            AudioManager.instance.PlaySoundEffects(animNum);
        }
        else { return; }
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

    public void Kill() 
    {
        _isDead = true;

        Quaternion lastRotation = playerModel.rotation;
        //playerModel.gameObject.SetActive(false);

        GameObject deathEffect = Instantiate(ragdoll, transform.position, lastRotation);
        if (_coin != null)
        {
            GameObject droppedItem = Instantiate(_coin, this.transform.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
        }

        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target) 
    {
        currentTarget = target;
    }

    public Player ReturnPlayer() 
    {
        return player;
    }

    public override bool IsDead() 
    {
        return _isDead;
    }

    public bool WakingUp()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Activate");
    }

    public bool IsHurt() 
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
    }

    public bool IsAttacking() 
    {
        return anim.GetBool("Attacking");
    }

    public bool CanMove()
    {
        return anim.GetBool("canMove");
    }

    public bool IsAntiAir() 
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("AntiAir");
    }

    public override void Revive()
    {
        base.Revive();
    }

    void OnDisable()
    {
        scumKilled -= Kill;
    }

    public void SendBackHome() 
    {
        currentTarget = activator;
    }

    public void HuntPlayer(Player playersNewPos)
    {
        player = playersNewPos;

        currentTarget = player.transform;
    }
}

