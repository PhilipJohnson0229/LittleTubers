using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform _targetA;
    public Transform _targetB;
    [SerializeField]
    private bool _switching;
    [SerializeField]
    private float _multiplier = 2f;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, Time.deltaTime * _multiplier);
        }
        else if (_switching == false) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, Time.deltaTime * _multiplier);
        }

        if (transform.position == _targetA.position)
        {
            _switching = true;
        } 
        else if(transform.position == _targetB.position)
        {
            _switching = false;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            other.transform.parent = this.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            other.transform.parent = null;
        }
    }
}
