using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private bool _goingDown = false;
    [SerializeField]
    private Transform _origin, _destination;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private bool destinationReached = true;

    public GameObject enemySwitch;
    public void CallElevator()
    {
        _goingDown = !_goingDown;
        destinationReached = false;
    }

    private void FixedUpdate()
    {
        if (!destinationReached)
        {
            if (_goingDown == true)
            {

                transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);
                if (transform.position == _destination.position) 
                {
                    destinationReached = true;
                }
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, _origin.position, Time.deltaTime * _speed);
                if (transform.position == _destination.position)
                {
                    destinationReached = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.gameObject.transform;
            enemySwitch.SetActive(true);

            enemySwitch.GetComponent<EnemySwitch>().AddRagdolls();
        }
    }

   
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
            enemySwitch.SetActive(false);
        }
    }
}
