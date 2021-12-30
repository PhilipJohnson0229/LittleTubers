using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNumPad : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            mainCamera.ActivateNumberPad();

            mainCamera.SetDoorToUnlock(door);

            Keypad.correctPassword += ObjectiveComplete;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            mainCamera.DeactivateNumberPad();
        }
    }

    public void ObjectiveComplete() 
    {
        Destroy(gameObject);
    }
}
