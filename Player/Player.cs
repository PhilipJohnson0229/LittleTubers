using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 5.0f;
    [SerializeField]
    private float _jumpForce = 5.0f;

    [SerializeField]
    private int _coin;

    [SerializeField]
    private int _health, maxHealth = 6;

    [SerializeField]
    private Vector3 _direction;

    public bool facingLeft = false;
    //comnbining projects
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private bool _isJumping;

    [SerializeField]
    private Ledge _ledge;

    [SerializeField]
    private bool _onLedge;

    [SerializeField]
    private bool _nearLadder;

    [SerializeField]
    private bool _LadderTopFlag = false;

    [SerializeField]
    private bool _onLadder;

    [SerializeField]
    private Ladder _currentLadder;

    [SerializeField]
    private bool _climbingOffLadder;

    [SerializeField]
    private CapsuleCollider _airCollider;

    [SerializeField]
    private Rigidbody _airBody;

    [SerializeField]
    private float _pushPower = 2.0f;

    [SerializeField]
    private BlockedPathEvent currentObstacle;

    [SerializeField]
    private Transform playerModel;
    public int health { get; set; }

    public float dataToTrack;

    public CinemachineVirtualCamera vcam;

    public GameObject bossHurtBox, ragdoll, kniferRagdoll, kniferRagdollDrop, currentKnifer;

    public bool isKnocking, canPickUpEventItem, canDropEventItem, carryingEventItem, canBeHurt;

    public Material playerMat;

    public float knockBackCounter = 1.5f;

    public Vector2 knockBackPower;

    public Color playerColor;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //combining projects
        _anim = GetComponentInChildren<Animator>();
        _airCollider = GetComponent<CapsuleCollider>();
        _airBody = GetComponent<Rigidbody>();
        playerMat.color = playerColor;
        canBeHurt = true;
        _health = maxHealth;
        if (_airCollider != null) { _airCollider.enabled = false; }
        if (_airBody != null) { _airBody.detectCollisions = false; }
        
        if (UIManager.instance != null) 
        {
            UIManager.instance.UpdateLives(_health);
            UIManager.instance.UpdateCoins(_coin);
        }
    }

    void Update()
    {
        CalculateMovement();

        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        if (_onLedge)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("Climb_Up");
            }
        }

        if (!_onLadder && _nearLadder)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_characterController.isGrounded && !_LadderTopFlag)
                {
                    ClimbLadder(_currentLadder.MountLadderPos(true, false));
                }
                else if (!_characterController.isGrounded && !_LadderTopFlag)
                {
                    _currentLadder.SetAirPosition(transform.position.x, transform.position.y);
                    ClimbLadder(_currentLadder.MountLadderPos(false, true));
                }
                else if (_characterController.isGrounded && _LadderTopFlag)
                {
                    ClimbLadder(_currentLadder.EnterFromTopPos());
                }
            }
        }

        if (canPickUpEventItem && !carryingEventItem) 
        {
            if (Input.GetKey(KeyCode.E)) 
            {
                PickUpEventItem();
                canPickUpEventItem = false;
                carryingEventItem = true;
            }
        }

        if (canDropEventItem && carryingEventItem) 
        {
            if (Input.GetKey(KeyCode.E))
            {
                canDropEventItem = false;
                DropOffEventItem();
            }
        }

        if (carryingEventItem) 
        {
            if (Input.GetKey(KeyCode.Space))
            {
                DropEventItem();
            }
        }
    }

    void CalculateMovement() 
    {
        Vector3 _facing = playerModel.localEulerAngles;

        //if grounded, we can jump
        if (_characterController.isGrounded && !isKnocking)
        {
            float _h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, _h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(_h));
            _anim.SetBool("Grounded", true);
            //setting our direction based on input
            //this will be the vector3 that will control the player
            if (_h != 0 && !_onLadder)
            {
                _facing.y = _direction.z > 0 ? 40 : 140;
                playerModel.localEulerAngles = _facing;
            }

            //combining projects
            if (_isJumping == true)
            {
                _isJumping = false;
                _anim.SetBool("Jump", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }        
        }

        if (_onLedge) 
        {
            _direction.y = 0;
        }

        //combining projects absorb code above
        if (_onLadder && !_climbingOffLadder)
        {
            float _v = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(0, _v, 0) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(_v));
            transform.Translate(_direction * Time.deltaTime);

            //dismount if player presses E key
            if (Input.GetKeyDown(KeyCode.E))
            {
                DismountLadder();
            }
        }
        else if(!_onLedge)
        {
            if (_airBody != null)
            {
                _airBody.detectCollisions = false;
            }

            if (_airCollider != null)
            {
                _airCollider.enabled = false;
            }
            //if we are not on a ladder then apply gravity and calculate movement on the ground
            if (!_characterController.isGrounded && !_onLadder)
            {
                float _h = Input.GetAxisRaw("Horizontal");
                _direction.z = _h * _speed;
                if (_h != 0)
                {
                    _facing.y = _direction.z > 0 ? 40 : 140;
                    playerModel.localEulerAngles = _facing;

                    _anim.SetBool("Grounded", false);
                }
            }

            
            _direction.y -= _gravity * Time.deltaTime;
            _characterController.Move(_direction * Time.deltaTime);

            if (!_characterController.isGrounded)
            {
                bossHurtBox.SetActive(true);
            }
            else 
            {
                bossHurtBox.SetActive(false);
            }
        }

        if (isKnocking)
        {
            float yStore = _direction.y;

            if (!facingLeft)
            {
                _direction = Vector3.back * knockBackPower.x;
            } else 
            {
                _direction = Vector3.forward * knockBackPower.x;
            }
            

            _direction.y = yStore;

            if (_characterController.isGrounded)
            {
                _direction.y = 0f;

            }

            _direction.y += Physics.gravity.y * Time.deltaTime * 5f;
            _characterController.Move(_direction * Time.deltaTime);
        }

        if (_facing.y == 40f)
        {
            facingLeft = false;
        }
        else if(_facing.y == 140f)
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
        _direction.y = 0f;
        _direction.y += _jumpForce;
        _direction.x = 0;
        _isJumping = true;
        _anim.SetBool("Jump", true);
    }

    public void EnemyJump()
    {
        _direction.y = 0f;
        _direction.y += _jumpForce * .3f;
        _direction.x = 0;
        _isJumping = true;
        _anim.SetBool("Jump", true);
    }

    public void CollectCoin(int _amount) 
    {
        _coin += _amount;
        UIManager.instance.UpdateCoins(_coin);
    }

    public void SpendCoin(int _amount)
    {
        _coin -= _amount;
        UIManager.instance.UpdateCoins(_coin);
    }

    public void Damage(int amount) 
    {
        if (canBeHurt) 
        {
            _health -= amount;
            StartCoroutine(Blink(knockBackCounter));
            UIManager.instance.UpdateLives(_health);

            int randNum = Random.Range(1,3);

            AudioManager.instance.PlaySoundEffects(randNum);
            _anim.SetBool("MouthOpen", true);
            _anim.SetInteger("FaceAnim", randNum);
            if (_health < 1)
            {
                UIManager.instance.LoadNextLevel(7);
            }
        }
        
    }

    public void Heal() 
    {
        _health = maxHealth;
        UIManager.instance.UpdateLives(_health);
    }

    public void Ensnare(Blamo blamo) 
    {
        blamo.Kill();
        
        Destroy(this.gameObject);
    }

    public void BossEnsnare(BossPhase1Controller blamo)
    {
        blamo.Kill();

        Destroy(this.gameObject);
    }

    public void Boss2Ensnare(BossPhase2Controller blamo)
    {
        blamo.Kill();

        Destroy(this.gameObject);
    }

    public int GetCoinsCount() 
    {
        return _coin;
    }

    public void GrabLedge(Vector3 _targetPos, Ledge _currentLedge)
    {
        Debug.Log("grabbing ledge?");
        _characterController.enabled = false;
        _isJumping = false;
        _anim.SetBool("Grab_Ledge", true);
        _anim.SetBool("Jump", false);
        _anim.SetFloat("Speed", 0f);
        _onLedge = true;
        transform.position = _targetPos;
        transform.localEulerAngles = _currentLedge.GetLedgeDirection();
        _ledge = _currentLedge; 
    }

    public void ClimbFromLedgeComplete()
    {
        transform.position = _ledge.GetStandPos();
        _anim.SetBool("Grab_Ledge", false);
        _isJumping = false;
        _anim.SetBool("Jump", false);
        _onLedge = false;
        _characterController.enabled = true;
    }

    public void LadderCheck(Ladder _ladder, bool _inRange, bool _topEntry)
    {
        if (!_onLadder)
        {
            _nearLadder = _inRange;
        }

        if (_topEntry)
        {
            _LadderTopFlag = true;
        }
        _currentLadder = _ladder;
    }

    public void ClimbLadder(Vector3 _targetPos)
    {
        _characterController.enabled = false;
        _airCollider.enabled = true;
        _airBody.detectCollisions = true;

        //snap player to position on ladder
        transform.position = _targetPos;
        Vector3 _facing = playerModel.localEulerAngles;
        _facing.y = -90f;
        playerModel.localEulerAngles = _facing;

        //play ladder climbing animation
        _nearLadder = false;
        _LadderTopFlag = false;
        _onLadder = true;
        _anim.SetBool("Jump", false);
        _anim.SetBool("Climb_Ladder", _onLadder);

        _currentLadder.OpenExits();
    }

    void ExitLadder(bool _isTopExit)
    {
        _anim.SetBool("Climb_Ladder", false);
        _climbingOffLadder = true;
        _anim.SetBool("Jump", false);
        if (_isTopExit)
        {
            _anim.SetTrigger("Exit_Ladder_Top");
        }
        else
        {
            _anim.SetTrigger("Exit_Ladder_Bottom");
        }
    }

    public void DismountLadder()
    {
        Debug.Log("trying to get off the ladder");
        //turn character controller back on
        _characterController.enabled = true;
        _airCollider.enabled = true;
        _airBody.detectCollisions = false;
        _airCollider.enabled = false;
        ClearLadderFlag();

        //correct animation
        _anim.SetBool("Climb_Ladder", false);
        _isJumping = true;
        _anim.SetBool("Jump", true);
        _onLadder = false;
        _nearLadder = false;
        _currentLadder.CloseExits();
    }

    public void ClearLadderFlag()
    {
        _LadderTopFlag = false;
    }

    public void StepOffLadder(bool _isTopExit)
    {
       

        if (_isTopExit)
        {
            transform.position = _currentLadder.GetStandPos();
        }
        else
        {
            transform.position = _currentLadder.GetStepDownPos();
        }
        
        _climbingOffLadder = false;
        _onLadder = false;
        _characterController.enabled = true;
        _currentLadder.CloseExits();
    }

    void Attack() 
    {
        _anim.SetTrigger("Attack");
    }

    public void speak(int animNum)
    {
        if (_anim.GetBool("MouthOpen") == false) 
        {
            _anim.SetBool("MouthOpen", true);
            _anim.SetInteger("FaceAnim", animNum);

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

    public void DropOffEventItem()
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

    void DropEventItem() 
    {
        carryingEventItem = false;

        if (facingLeft)
        {
            Instantiate(kniferRagdollDrop, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 2), Quaternion.identity);
        }
        else 
        {
            Instantiate(kniferRagdollDrop, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 2), Quaternion.identity);
        }
        
        kniferRagdoll.SetActive(false);
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
            #region attempted ragdoll

            /*Rigidbody[] rigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();

            ragdoll.transform.position = this.transform.position;

            ragdoll.SetActive(true);

            foreach (Rigidbody rb in rigidbodies)
            {
                rb.AddExplosionForce(10, ragdoll.transform.position, 50f, 70f, ForceMode.Impulse);
              
            }
            //ragdoll.AddTorque(new Vector3(5f, 0, 0), ForceMode.Impulse);
            Destroy(gameObject);
            UIManager.instance.LoadNextLevel(0);*/
            #endregion
        }
    }
    
    public void Freeze()
    {
        _anim.SetBool("CanMove", true);
    }

    public void Unfreeze() 
    {
        _anim.SetBool("CanMove", false);
    }

     private void OnControllerColliderHit(ControllerColliderHit _hit)
   {
       if (_hit.transform.tag == "Moving Box") 
       {
           Rigidbody _box = _hit.collider.GetComponent<Rigidbody>();

           if (_box != null) 
           {
               Vector3 _pushDirection = new Vector3(0, 0, _hit.moveDirection.z);

               _box.velocity = _pushDirection * _pushPower;
           }
       }
   }
}
