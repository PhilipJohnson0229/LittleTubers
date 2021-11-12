using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlamo : MonoBehaviour
{
    public GameObject blamoCutsceneObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            blamoCutsceneObject.SetActive(true);

            this.gameObject.SetActive(false);
        }

    }
}
