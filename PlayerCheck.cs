using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public GameObject thisJumpPad;
    public BlamosLedge ledge;
    public bool LevelTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            LevelTrigger = true;
            if (LevelTrigger) 
            {
                Blamo blamo = FindObjectOfType<Blamo>();
                
                if (blamo != null) 
                {
                    blamo.jumpPad = thisJumpPad;
                    blamo.AssignLedge(ledge);

                    if (!blamo.HeightLevel() && !blamo.IsOnPlatform())
                    {
                        blamo.ChangeLevel();
                    }
                }
               
                LevelTrigger = false;
            }

            thisJumpPad.SetActive(true);
        }

        if (other.tag == "HellReaper") 
        {
            Blamo blamo = FindObjectOfType<Blamo>();

            thisJumpPad.SetActive(false);

            blamo.AssignPlatform();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelTrigger = true;
            if (LevelTrigger)
            {
                Blamo blamo = FindObjectOfType<Blamo>();
               
                if (blamo != null) 
                {
                    blamo.jumpPad = null;
                    if (blamo.HeightLevel())
                    {
                        blamo.ResetLevel();
                        blamo.SetMovementState();
                    }
                }
                
                LevelTrigger = false;
            }
        }

        if (other.tag == "HellReaper")
        {
            Blamo blamo = FindObjectOfType<Blamo>();

            if (blamo != null) 
            {

                blamo.ResetPlatform();

                thisJumpPad.SetActive(false);
            }     
        }
    }
}
