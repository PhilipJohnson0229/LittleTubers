using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{

    [SerializeField]
    private GameObject _bullet;

    public Vector3 _offset;

    public bool playerIsTooClose = false;

    public Transform salvo; 
    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
    }

    public override void Update()
    {
        if (playerIsTooClose) { return; }
        else if(_player != null)
        {         

            Vector3 _direction = transform.localPosition - _player.transform.localPosition;

            if (_direction.z > 0)
            {
                salvo.rotation = Quaternion.Euler( 0, 180f, 0);
            }
            else if (_direction.z < 0)
            {

                salvo.rotation = Quaternion.Euler(0, 0, 0);
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
        if (other.tag == "Player") 
        {
            playerIsTooClose = true;
            _anim.SetBool("PlayerTooClose", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsTooClose = false;

            _anim.SetBool("PlayerTooClose", false);
        }
    }
    // Start is called before the first frame update

}
