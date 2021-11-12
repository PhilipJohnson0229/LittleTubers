using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject _cp_On, _cp_Off;
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
                allCp[i]._cp_Off.SetActive(true);
                allCp[i]._cp_On.SetActive(false);
                allCp[i].GetComponent<Rigidbody>().detectCollisions = true;
            }

            this.GetComponent<Rigidbody>().detectCollisions = false;

            _cp_Off.SetActive(false);
            _cp_On.SetActive(true);
        }
    }
}
