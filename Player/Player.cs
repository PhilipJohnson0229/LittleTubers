using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour, IDamageable
{
    private CharacterController characterController;

    [SerializeField]
    private Vector3 direction;
    
    private Animator anim;

    private bool usingLadderFirstTime = true, facingLeft = false, ladderTopFlag = false;

    private Ledge ledge;

    [SerializeField]
    private bool onLedge, nearLadder, onLadder, climbingOffLadder, isJumping;

    private bool isKnocking, canPickUpEventItem, canDropEventItem, carryingEventItem, canBeHurt;

    private Ladder currentLadder;

    private CapsuleCollider airCollider;

    private Rigidbody airBody;

    private float pushPower = 2.0f;

    [SerializeField]
    private BlockedPathEvent currentObstacle;

    [SerializeField]
    private Transform playerModel;

    private UIManager uiManager;

    public GameObject keyVisual;

    public int health { get; set; }

    public int healSoundEffect;

    public GameObject bossHurtBox, ragdoll, kniferRagdoll, kniferRagdollDrop, currentKnifer;

    public Material playerMat;

    private float knockBackCounter = 1.5f;

    public Vector2 knockBackPower;

    public Color playerColor;

    public PlayerData playerData;

  

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        airCollider = GetComponent<CapsuleCollider>();
        airBody = GetComponent<Rigidbody>();
        uiManager = FindObjectOfType<UIManager>();
        playerMat.color = playerColor;
        canBeHurt = true;
        health = playerData.ReturnHealth();

        if (airCollider != null) { airCollider.enabled = false; }
        if (airBody != null) { airBody.detectCollisions = false; }
        
        if (uiManager != null) 
        {
            uiManager.UpdateLives(health);
        }
    }

    void Update()
    {
        CalculateMovement();

        if (onLedge)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("Climb_Up");
            }
        }

        if (!onLadder && nearLadder)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (characterController.isGrounded && !ladderTopFlag)
                {
                    ClimbLadder(currentLadder.MountLadderPos(true, false));
                }
                else if (!characterController.isGrounded && !ladderTopFlag)
                {
                    currentLadder.SetAirPosition(transform.position.x, transform.position.y);
                    ClimbLadder(currentLadder.MountLadderPos(false, true));
                }
                else if (characterController.isGrounded && ladderTopFlag)
                {
                    ClimbLadder(currentLadder.EnterFromTopPos());
                }
            }
        }

        if (canPickUpEventItem) 
        {
            if (Input.GetKey(KeyCode.E) && !carryingEventItem) 
            {
                PickUpEventItem();
                canPickUpEventItem = false;
                carryingEventItem = true;
            }
        }

        if (carryingEventItem) 
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dropEventItem();
            }

            if (canDropEventItem) 
            {
                if (Input.GetKey(KeyCode.E))
                {
                    setCanDropEventItem(false);
                    installEventItem();
                }
            }           
        }
    }

    void CalculateMovement() 
    {
        Vector3 facing = playerModel.localEulerAngles;

        //if grounded, we can jump
        if (characterController.isGrounded && !isKnocking && !onLadder)
        {
            float h = Input.GetAxisRaw("Horizontal");
            direction = new Vector3(0, 0, h) * playerData.getSpeed();
            anim.SetFloat("Speed", Mathf.Abs(h));
            anim.SetBool("Grounded", true);
            //setting our direction based on input
            //this will be the vector3 that will control the player
            if (h != 0 && !onLadder)
            {
                facing.y = direction.z > 0 ? 40 : 140;
                playerModel.localEulerAngles = facing;
            }

            if (getIsJumping() == true)
            {
                setIsJumping(false);
                anim.SetBool("Jump", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }        
        }

        direction.y = onLedge ? 0 : direction.y;
       
        if (onLadder && !climbingOffLadder)
        {
            float _v = Input.GetAxisRaw("Vertical");
            direction = new Vector3(0, _v, 0) * playerData.getSpeed();
            anim.SetFloat("Speed", Mathf.Abs(_v));
            transform.Translate(direction * Time.deltaTime);

            //dismount if player presses E key
            if (Input.GetKeyDown(KeyCode.E))
            {
                DismountLadder();
            }
        }
        
        if(!onLedge && characterController.enabled)
        {
            if (airBody != null)
            {
                airBody.detectCollisions = false;
            }

            if (airCollider != null)
            {
                airCollider.enabled = false;
            }
            //if we are not on a ladder then apply gravity and calculate movement on the ground
            if (!characterController.isGrounded && !onLadder)
            {
                float _h = Input.GetAxisRaw("Horizontal");
                direction.z = _h * playerData.getSpeed();
                if (_h != 0)
                {
                    facing.y = direction.z > 0 ? 40 : 140;
                    playerModel.localEulerAngles = facing;

                    anim.SetBool("Grounded", false);
                }
            }

            
            direction.y -= playerData.getGravity() * Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stuck"))
            {
                direction.z = 0;
            }
            characterController.Move(direction * Time.deltaTime);

            if (characterController.isGrounded && direction.y < 0)
            {
                direction.y = 0;
            }

            if (!characterController.isGrounded)
            {
                bossHurtBox.SetActive(true);
            }
            else 
            {
                bossHurtBox.SetActive(false);
            }
        }

        if (facing.y == 40f)
        {
            facingLeft = false;
        }
        else if(facing.y == 140f)
        {
            facingLeft = true;
        }  
    }

    IEnumerator Blink(float timer) 
    {
        bool blinking = false;
        canBeHurt = false;
        isKnocking = true;
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

            yield return null;
        }
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        canBeHurt = true;
        isKnocking = false;
        playerMat.color = playerColor;
        blinking = false;     
    }

    public void Jump() 
    {
        AudioManager.instance.PlaySoundEffects(playerData.jumpSound);
        direction.y = 0f;
        direction.y += playerData.getJumpForce();
        direction.x = 0;
        setIsJumping(true);
        anim.SetBool("Jump", true);
    }

    public void EnemyJump()
    {
        AudioManager.instance.PlaySoundEffects(playerData.enemyJumpSound);
        direction.y = 0f;
        direction.y += playerData.getJumpForce() * .6f;
        direction.x = 0;
        setIsJumping(true);
        anim.SetBool("Jump", true);
    }

    public void TrampolineJump(int jumpSound)
    {
        AudioManager.instance.PlaySoundEffects(jumpSound);
        direction.y = 0f;
        direction.y += playerData.getJumpForce() * 1.5f;
        direction.x = 0;
        setIsJumping(true);
        anim.SetBool("Jump", true);
    }

    public void Damage(int amount) 
    {
        if (playerData.isInHell() == false)
        {
            playerData.Damage(amount);
            health = playerData.ReturnHealth();
            // StartCoroutine(Blink(knockBackCounter));
            uiManager.UpdateLives(health);

            int randNum = Random.Range(1, 3);

            AudioManager.instance.PlaySoundEffects(randNum);
            anim.SetBool("MouthOpen", true);
            anim.SetInteger("FaceAnim", randNum);

            if (health < 1)
            {
                playerData.SendToHell();
            }
        }
        else 
        {
            HellSpawn();
        }  
    }

    public void HellSpawn() 
    {
        uiManager.HellSpawn();
        playerData.SplatEffect(transform.position);
        gameObject.SetActive(false);
    }

    public void SendToHell()
    {
        uiManager.CheckLifeOffering();
        ragdoll.transform.position = transform.position;
        playerModel.gameObject.SetActive(false);
    }


    public void Heal() 
    {
        playerData.setHealth(6);
        health = playerData.ReturnHealth();
        AudioManager.instance.PlaySoundEffects(healSoundEffect);
        uiManager.UpdateLives(health);
    }

    public void Ensnare(Blamo blamo) 
    {
        blamo.Kill();
        PitFall();
    }

    public void RushEnsnare(RushingBlamo blamo)
    {
        blamo.Kill();
        PitFall();
    }

    public void BossEnsnare(BossPhase1Controller blamo)
    {
        blamo.Kill();

        gameObject.SetActive(false);
    }

    public void Boss2Ensnare(BossPhase2Controller blamo)
    {
        blamo.Kill();

        gameObject.SetActive(false);
    }

    public void PitFall() 
    {
        uiManager.OfferAChoice();
        gameObject.SetActive(false);
    }

    public void GrabLedge(Vector3 _targetPos, Ledge _currentLedge)
    {
        
        characterController.enabled = false;
        isJumping = false;
        anim.SetBool("Grab_Ledge", true);
        anim.SetBool("Jump", false);
        anim.SetFloat("Speed", 0f);
        onLedge = true;
        transform.position = _targetPos;
        playerModel.localEulerAngles = _currentLedge.GetLedgeDirection();
        ledge = _currentLedge; 
    }

    public void ClimbFromLedgeComplete()
    {
        transform.position = ledge.GetStandPos();
        anim.SetBool("Grab_Ledge", false);
        isJumping = false;
        anim.SetBool("Jump", false);
        onLedge = false;
        characterController.enabled = true;
    }

    public void LadderCheck(Ladder _ladder, bool _inRange, bool _topEntry)
    {
        if (!onLadder)
        {
            nearLadder = _inRange;
        }

        if (_topEntry)
        {
            ladderTopFlag = true;
        }
        currentLadder = _ladder;

        if (usingLadderFirstTime) 
        {
            uiManager.Notification("Press E");
            usingLadderFirstTime = false;
        }
        
    }

    public void ClimbLadder(Vector3 _targetPos)
    {
        characterController.enabled = false;
        airCollider.enabled = true;
        airBody.detectCollisions = true;

        //snap player to position on ladder
        transform.position = _targetPos;
        Vector3 facing = playerModel.localEulerAngles;
        facing.y = -90f;
        playerModel.localEulerAngles = facing;

        //play ladder climbing animation
        nearLadder = false;
        ladderTopFlag = false;
        onLadder = true;
        anim.SetBool("Jump", false);
        anim.SetBool("Climb_Ladder", onLadder);

        currentLadder.OpenExits();
    }

    void ExitLadder(bool _isTopExit)
    {
        anim.SetBool("Climb_Ladder", false);
        climbingOffLadder = true;
        anim.SetBool("Jump", false);
        if (_isTopExit)
        {
            anim.SetTrigger("Exit_Ladder_Top");
        }
        else
        {
            anim.SetTrigger("Exit_Ladder_Bottom");
        }
    }

    public void DismountLadder()
    {
        Debug.Log("trying to get off the ladder");
        //turn character controller back on
        characterController.enabled = true;
        airCollider.enabled = true;
        airBody.detectCollisions = false;
        airCollider.enabled = false;
        ClearLadderFlag();

        //correct animation
        anim.SetBool("Climb_Ladder", false);
        isJumping = true;
        anim.SetBool("Jump", true);
        onLadder = false;
        nearLadder = false;
        currentLadder.CloseExits();
    }

    public void ClearLadderFlag()
    {
        ladderTopFlag = false;
    }

    public void StepOffLadder(bool _isTopExit)
    {
        if (_isTopExit)
        {
            transform.position = currentLadder.GetStandPos();
        }
        else
        {
            transform.position = currentLadder.GetStepDownPos();
        }
        
        climbingOffLadder = false;
        onLadder = false;
        characterController.enabled = true;
        currentLadder.CloseExits();
    }

    void Attack() 
    {
        anim.SetTrigger("Attack");
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

    public void SetCurrentObstacle(BlockedPathEvent _currentObstacle) 
    {
        currentObstacle = _currentObstacle;
    }

    public void PickUpEventItem() 
    {
        kniferRagdoll.SetActive(true);
        
        if (currentKnifer != null) 
        {
            Destroy(currentKnifer);
        }

        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        //have the animator play a raised arm animation and mask out the legs
    }

    public void installEventItem()
    {
        kniferRagdoll.SetActive(false);
        
        if (currentObstacle != null)
        {
            Animator currentObstAnim = currentObstacle.GetComponent<Animator>();
            currentObstacle.ActivateProp();
            carryingEventItem = false;
            if (currentObstAnim != null)
            {
                currentObstAnim.SetBool("Activate", true);
            }
        }

        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        //have the animator play a raised arm animation and mask out the legs
    }

    public void dropEventItem() 
    {
        carryingEventItem = false;
        
        if (facingLeft)
        {
            Instantiate(kniferRagdollDrop, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        }
        else 
        {
            Instantiate(kniferRagdollDrop, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        }
        
        kniferRagdoll.SetActive(false);
    }

    public void Stun() 
    {
        anim.Play("Stuck");
        anim.SetBool("Stun", true);
        //anim.ResetTrigger("Stun");
    }

    public void UnStun() 
    {
        anim.SetBool("Stun", false);
    }

    public void PickUpKey() 
    {
        playerData.setHasKey(true);

        keyVisual.SetActive(true);
    }

    public void UseKey() 
    {
        playerData.setHasKey(false);

        keyVisual.SetActive(false);
    }

    void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Ladder_Exit_Top"))
        {
            ExitLadder(true);
        }
        else if (_other.CompareTag("Ladder_Exit_Bottom"))
        {
            ExitLadder(false);
        }

        if (_other.tag == "KillerCar") 
        {
            Damage(2);
           
        }
    }

     private void OnControllerColliderHit(ControllerColliderHit _hit)
     {
       if (_hit.transform.tag == "Moving Box") 
       {
           Rigidbody _box = _hit.collider.GetComponent<Rigidbody>();

           if (_box != null) 
           {
               Vector3 _pushDirection = new Vector3(0, 0, _hit.moveDirection.z);

               _box.velocity = _pushDirection * pushPower;
           }
       }
     }

    private void OnEnable()
    {
        playerData.playerDied += SendToHell;

    }

    private void OnDisable()
    {
        playerData.playerDied -= SendToHell;

    }

    public bool CanPickUpEventItem() 
    {
        return canPickUpEventItem;
    }

    public void setCanPickUpEventItem(bool canPickUpItem) 
    {
        this.canPickUpEventItem = canPickUpItem;
    }

    public void setCanDropEventItem(bool canDropItem)
    {
        this.canDropEventItem = canDropItem;
    }

    public void setCarryingEventItem(bool value)
    {
        this.carryingEventItem = value;
    }

    public void setIsJumping(bool value) 
    {
        isJumping = value;
    }

    public bool getIsJumping() 
    {
        return isJumping;
    }

    public bool getCarryingEventItem() 
    {
        return carryingEventItem;
    }
}
