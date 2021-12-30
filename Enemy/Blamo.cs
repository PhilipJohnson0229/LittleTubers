using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blamo : Enemy
{
    public int health { get; set; }
    [SerializeField]
    protected Vector3 _direction;
    protected bool facingLeft;
    protected BoxCollider boxCol;
    protected float h = 0;

    public Transform playerModel;

    protected float attackDistance = 2f;
    [SerializeField]
    protected float targetDistance;
    [SerializeField]
    protected bool closeEnoughToAttack = false;
    [SerializeField]
    protected Transform trackedTarget;
    protected bool attacking = false;

    public float distanceTracker;
    protected Rigidbody rb;

    //attempting to make blamo jump onto a platform\
    [SerializeField]
    protected bool playerIsAboveMe = false;
    [SerializeField]
    protected bool onPlatform = false;

    [SerializeField]
    private bool movingTowardsJumpPad = false;
 
    [SerializeField]
    private bool jumpingTowardsLedge = false;

    [SerializeField]
    private BlamosLedge ledge;

    private bool onLedge;

    public GameObject jumpPad;

    public float gravity = 5.0f;

    [SerializeField]
    protected GameObject groundChecker, ragdollEffect;
  
    public GameObject floorChecker;

    [SerializeField]
    protected bool isJumping;

    [SerializeField]
    protected bool isGrounded;

    [SerializeField]
    protected bool justLanded = false;

    public Transform[] edgeChecker;
    public bool nearEdge;
    Vector3 ledgeDirection;

    public GameObject hitBox;
    public GameObject deathDUmmy;
    public override void Init()
    {
        base.Init();
        health = base._health;
        rb = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        trackedTarget = _player.transform;
        groundChecker.SetActive(false);
        isGrounded = true;
    }

    public override void Movement()
    {
        _direction = transform.localPosition - trackedTarget.localPosition;
        Vector3 _facing = transform.localEulerAngles;
        targetDistance = Mathf.Abs(_direction.z);
        
        Vector3 velocity = new Vector3(0, 0, h) * _speed;

        _anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.z));

        if (!onLedge)
        {
            //Main movement code
            if (!closeEnoughToAttack && !jumpingTowardsLedge)
            {
                attacking = false;
                _anim.SetBool("InCombat", false);
                if (trackedTarget != null) 
                {
                    transform.Translate(velocity * Time.deltaTime);
                }
            }
            else
            {
                attacking = true;
                _anim.SetBool("InCombat", true);
            }

            if (!movingTowardsJumpPad && targetDistance <= attackDistance && targetDistance > 0.5f && isGrounded)
            {
                closeEnoughToAttack = true;
            }
            else
            {
                closeEnoughToAttack = false;
            }

            if (movingTowardsJumpPad)
            {
                trackedTarget = jumpPad.transform;
            }
            else if (!jumpingTowardsLedge)
            {
                trackedTarget = _player.transform;
            }

            if (_direction.z > 0)
            {
                _facing.y = 140f;
                playerModel.localEulerAngles = _facing;
                facingLeft = true;
                h = -1f;

                //Vector3 _attackPosition = new Vector3(transform.position.x, transform.position.y, (transform.localPosition.z - _player.transform.localPosition.z));
            }
            else if (_direction.z < 0)
            {
                _facing.y = 40f;
                playerModel.localEulerAngles = _facing;
                facingLeft = false;
                h = 1f;

            }
        }
        else
        {
            return;
        }

        if (playerIsAboveMe && !isJumping && !onPlatform)
        {
            movingTowardsJumpPad = true;
        }

        if (attacking)
        {
            rb.velocity = Vector3.zero;
        }
        else { hitBox.SetActive(false); }

        if (justLanded) 
        {    
            rb.useGravity = false;
            isJumping = false;
            jumpingTowardsLedge = false;
            isGrounded = true;
            rb.velocity = Vector3.zero;
            justLanded = false;
        }

        if (isGrounded) 
        {
            groundChecker.SetActive(false);
        }

        if (isJumping)
        {
            velocity.y -= gravity * Time.deltaTime;
            hitBox.SetActive(false);
            FloorCheck();
        }
  

        EdgeCheck();

        distanceTracker = targetDistance;
    }

    public void Jump()
    {
        Vector3 jumpForce = new Vector3(0,8,0);
        rb.velocity += jumpForce;
        rb.useGravity = true;
        isJumping = true;
        isGrounded = false;
        trackedTarget = _player.transform;
        _anim.SetBool("Grounded", false);
        movingTowardsJumpPad = false;
    }

    public void ChangeLevel() 
    {
        playerIsAboveMe = true;
    }

    public void ResetLevel() 
    {
        playerIsAboveMe = false;
    }

    public bool HeightLevel() 
    {
        return playerIsAboveMe;
    }

    public bool GetMovementState()
    {
        return movingTowardsJumpPad;
    }

    public void SetMovementState() 
    {
        movingTowardsJumpPad = false;
    }

    public bool IsOnPlatform()
    {
        return onPlatform;
    }

    public void AssignPlatform() 
    {
        onPlatform = true;
    }

    public void ResetPlatform()
    {
        onPlatform = false;
    }
    //call this from an animation behaviour

    public void AssignLedge(BlamosLedge theLedge)
    {
        ledge = theLedge;
    }

    public void ReachForLedge()
    {
        isJumping = true;
        isGrounded = false;
        GrabLedge(ledge._targetPosition, ledge);
    }

    public void JumpForLedgeFromPad()
    {
        _anim.SetBool("JumpingForLedge", true);
        
        hitBox.SetActive(false);
    }

    public void GrabLedge(Vector3 _targetPos, BlamosLedge _currentLedge)
    {
        
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        isJumping = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetBool("Grounded", false);
        _anim.SetBool("JumpingForLedge", false);
        onLedge = true;
        transform.position = _targetPos;
        playerModel.localEulerAngles = _currentLedge.GetLedgeDirection();
      
        ledge = _currentLedge;
    }

    public void ClimbFromLedgeComplete()
    {
        transform.position = ledge.GetStandPos();
        isGrounded = true;
        _anim.SetBool("GrabLedge", false); 
        _anim.SetBool("Jumping", false);
        _anim.SetBool("Grounded", true);
        jumpingTowardsLedge = false;
        onLedge = false;
        isJumping = false;
        movingTowardsJumpPad = false;
        playerIsAboveMe = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        ledge = null;
        trackedTarget = _player.transform;
    }

    public void EdgeCheck() 
    {
        
        RaycastHit hit;
        Ray ray = new Ray(floorChecker.transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance > 4)
            {
                _anim.SetBool("Jumping", true);
                _anim.SetBool("Grounded", false);
            }
        }

    }

    public void FloorCheck()
    { 
        RaycastHit hit;

        Ray ray = new Ray(this.transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance > 1)
            {
                groundChecker.SetActive(true);
            }
        }
    }

    public void SetGroundedState() 
    {
        justLanded = true;
        isJumping = false;
        attacking = false;
        _anim.SetBool("Grounded", true);
        _anim.SetBool("Jumping", false);
        transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
        rb.velocity = Vector3.zero;
        
    }

    public void Kill()
    {
        deathDUmmy.SetActive(true);
        _anim.SetBool("Kill", true);
    }

    public void Swallow() 
    {
        deathDUmmy.SetActive(false);
        UIManager.instance.LoadNextLevel(1);
    }

    public void Defeat() 
    {
        Rigidbody[] rigidbodies = ragdollEffect.GetComponentsInChildren<Rigidbody>();
       
        ragdollEffect.transform.position = this.transform.position;
        
        ragdollEffect.SetActive(true);

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(15, ragdollEffect.transform.position, 50f, 70f, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }


}
