using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushingBlamo : Enemy
{
    protected Vector3 direction;
    protected bool facingLeft;
    protected BoxCollider boxCol;
    protected float h = 0;

    private float attackDistance = 2f;
    [SerializeField]
    private float targetDistance;
    [SerializeField]
    private bool closeEnoughToAttack = false;
    [SerializeField]
    private Transform trackedTarget;
    private bool attacking = false;

    public float distanceTracker;
    protected Rigidbody rb;

    public float gravity = 5.0f;
    
    public override void Init()
    {
        base.Init();
        rb = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        trackedTarget = player.transform;
    }

   
    public override void Movement()
    {
        direction = transform.position - trackedTarget.position;
        Vector3 facing = transform.localEulerAngles;
        targetDistance = Mathf.Abs(direction.z);

        Vector3 velocity = new Vector3(0, 0, h) * _speed;

        //_anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.z));

        if (!closeEnoughToAttack)
        {
            attacking = false;
            anim.SetBool("InCombat", false);
            if (trackedTarget != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("Start") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Recover"))
            {
                transform.Translate(velocity * Time.deltaTime);
            }
        }
        else
        {
            attacking = true;
            anim.SetBool("InCombat", true);
        }

        if (targetDistance <= attackDistance && targetDistance > 0.5f && !anim.GetBool("InCombat"))
        {
            closeEnoughToAttack = true;
        }
        else
        {
            closeEnoughToAttack = false;
        }

        if (direction.z > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            facing.y = 180f;
            playerModel.localEulerAngles = facing;
            facingLeft = true;
            h = -1f;

            //Vector3 _attackPosition = new Vector3(transform.position.x, transform.position.y, (transform.localPosition.z - _player.transform.localPosition.z));
        }
        else if (direction.z < 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            facing.y = 0;
            playerModel.localEulerAngles = facing;
            facingLeft = false;
            h = 1f;

        }


        distanceTracker = targetDistance;
    }

    public void Kill()
    {
        //deathDUmmy.SetActive(true);
        anim.SetBool("Kill", true);
    }

    public void Swallow()
    {
        //deathDUmmy.SetActive(false);
        player.playerData.SendToHell();
        trackedTarget = null;
       
    }

    public void SetTrackedTarget(Transform tracked) 
    {
        trackedTarget = tracked;
        anim.SetBool("Kill", false);
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InTheWay") 
        {
            anim.SetTrigger("HitCar");
        }
    }
}
