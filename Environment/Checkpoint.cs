using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject animatedObject;
    [SerializeField]
    private DeadZone _deadZone;

    void Start() 
    {
        _deadZone = GameObject.FindObjectOfType<DeadZone>();
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            //tell deadzone to set spawn point here
            if (_deadZone != null && player != null)
            {
                _deadZone.SetSpawnPoint(this.gameObject);
                _deadZone.SetPlayer(player);
            }

            Checkpoint[] allCp = FindObjectsOfType<Checkpoint>();

            for (int i = 0; i < allCp.Length; i++)
            {
                allCp[i].animatedObject.GetComponent<Animator>().SetBool("Active",false);
                allCp[i].GetComponent<Rigidbody>().detectCollisions = true;
            }

            this.GetComponent<Rigidbody>().detectCollisions = false;

            animatedObject.GetComponent<Animator>().SetBool("Active", true);
        }
    }
}
