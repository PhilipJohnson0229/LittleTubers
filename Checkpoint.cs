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
        _deadZone = GameObject.Find("Dead Zone").GetComponent<DeadZone>();
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //tell deadzone to set spawn point here
            if (_deadZone != null)
            {
                _deadZone.SetSpawnPoint(this.gameObject);
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
