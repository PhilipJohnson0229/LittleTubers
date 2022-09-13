using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlamo : MonoBehaviour
{
    public GameObject blamoCutsceneObject;

    public GameObject glassIsCool;

    public bool usesGlass = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            blamoCutsceneObject.SetActive(true);

            this.gameObject.SetActive(false);

            if (usesGlass) 
            {
                if (glassIsCool != null) 
                {
                    glassIsCool.GetComponent<Animator>().SetBool("Shatter", true);
                }
            }
        }

    }
}
