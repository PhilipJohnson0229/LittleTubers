using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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
    private int _health = 3;

    [SerializeField]
    private Vector3 _direction;
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

    public int health { get; set; }

    public float dataToTrack;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //combining projects
        _anim = GetComponentInChildren<Animator>();
        _airCollider = GetComponent<CapsuleCollider>();
        _airBody = GetComponent<Rigidbody>();
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
    }

    void CalculateMovement() 
    { 
        //if grounded, we can jump
        if (_characterController.isGrounded)
        {
            float _h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, _h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(_h));
            _anim.SetBool("Grounded", true);
            //setting our direction based on input
            //this will be the vector3 that will control the player
            if (_h != 0 && !_onLadder)
            {
                Vector3 _facing = transform.localEulerAngles;
                _facing.y = _direction.z > 0 ? 40 : 140;
                transform.localEulerAngles = _facing;
            }

            //combining projects
            if (_isJumping == true)
            {
                _isJumping = false;
                _anim.SetBool("Jump", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpForce;
                _isJumping = true;
                _anim.SetBool("Jump", true);
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
                    Vector3 _facing = transform.localEulerAngles;
                    _facing.y = _direction.z > 0 ? 40 : 140;
                    transform.localEulerAngles = _facing;

                    _anim.SetBool("Grounded", false);
                }
            }

            
            _direction.y -= _gravity * Time.deltaTime;
            _characterController.Move(_direction * Time.deltaTime);
        } 
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
        _health -= amount;
        UIManager.instance.UpdateLives(_health);
        UIManager.instance.Notification("Player was damaged!");
        if (_health < 1)
        {
            UIManager.instance.LoadNextLevel(1);
        }
    }

    public int GetCoinsCount() 
    {
        return _coin;
    }

    public void GrabLedge(Vector3 _targetPos, Ledge _currentLedge)
    {
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
        Vector3 _facing = transform.localEulerAngles;
        _facing.y = -90f;
        transform.localEulerAngles = _facing;

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
        Debug.Log("trying to step off the ladder");

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
    }

    /* private void OnControllerColliderHit(ControllerColliderHit _hit)
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
   }*/
}
