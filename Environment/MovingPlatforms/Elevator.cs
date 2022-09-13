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
    private bool destinationReached = true, doorClosed = false;

    public int elevatorStartSound;

    public GameObject door, doorCollider;

    public GameObject enemySwitch;
    public void CallElevator()
    {
        _goingDown = !_goingDown;
        destinationReached = false;

        StartCoroutine(ElevatorMove());
    }

    private void FixedUpdate()
    {
        
        /*else 
        {
            if (doorClosed) 
            {
                doorClosed = false;
                door.SetActive(false);
                doorCollider.SetActive(false);
            }
           
        }*/
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

    IEnumerator ElevatorMove() 
    {
        door.SetActive(true);
        doorCollider.SetActive(true);
        AudioManager.instance.PlaySoundEffects(elevatorStartSound);
        gameObject.GetComponent<AudioSource>().Play();

        while (!destinationReached)
        {
           

            if (_goingDown == true)
            {

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination.localPosition, Time.deltaTime * _speed);
                if (transform.localPosition == _destination.localPosition)
                {
                    destinationReached = true;
                }

                yield return null;
            }
            else
            {
                
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _origin.localPosition, Time.deltaTime * _speed);
                if (Mathf.Abs(transform.localPosition.y - _origin.localPosition.y) < 0.1)
                {
                    destinationReached = true;
                }

                yield return null;
            }
        }

        gameObject.GetComponent<AudioSource>().Stop();
        AudioManager.instance.PlaySoundEffects(elevatorStartSound);
        door.SetActive(false);
        doorCollider.SetActive(false);
        Debug.Log("Trying to stop");
        destinationReached = true;
    }
}
