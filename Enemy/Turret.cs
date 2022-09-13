using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy, IDamageable
{

    [SerializeField]
    private GameObject _bullet;

    public Vector3 _offset;

    [SerializeField]
    private bool playerIsTooClose = false, hasTurtleShell = false;

    public Transform salvo;

    private Vector3 direction;

    private int deathJumpsLeft = 1;

    public int health { get; set; }

    public override void Init()
    {
       
        player = FindObjectOfType<Player>();
    }

    public override void Update()
    {
        if (hasTurtleShell) 
        {
            if (playerIsTooClose) { return; }
        }
        
        if(player != null)
        {         

            direction = transform.position - player.transform.position;
            if (hasTurtleShell)
            {
                if (direction.z > 0)
                {
                    salvo.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else if (direction.z < 0)
                {

                    salvo.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else 
            {
                if (direction.z > 0)
                {
                    playerModel.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else if (direction.z < 0)
                {

                    playerModel.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            
        }
    }

    public override void Attack() 
    {
        
        Vector3 _facing = transform.localEulerAngles;
        
        transform.localEulerAngles = _facing;
       
        Instantiate(_bullet, salvo.position + _offset, salvo.transform.rotation);
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (hasTurtleShell)
        {
            if (other.tag == "Player")
            {
                playerIsTooClose = true;
                anim.SetBool("PlayerTooClose", true);
            }
        }
        else 
        {
            if (other.tag == "PlayerHurtBox") 
            {
                Damage(1);

                if (player != null) 
                {
                    if (deathJumpsLeft > 0) 
                    {
                        player.EnemyJump();

                        if (_isDead) 
                        {
                            deathJumpsLeft --;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasTurtleShell) 
        {
            if (other.tag == "Player")
            {
                playerIsTooClose = false;

                anim.SetBool("PlayerTooClose", false);
            }
        }
    }

    public void Damage(int amount)
    {
        if (!_isDead)
        {

            _health--;

            anim.SetTrigger("Hurt");
            

            if (_health <= 0)
            {
                _health = 0;
                _isDead = true;

                anim.SetBool("Dead", true);
               
                //deathEffect.transform.parent = this.transform;
                Destroy(gameObject, 1.5f);

            }
        }
    }
    // Start is called before the first frame update

}
