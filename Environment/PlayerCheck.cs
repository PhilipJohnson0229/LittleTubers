using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public GameObject[] jumpPads;
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
                    blamo.jumpPad = JumpPadChosen();
                    //blamo.AssignLedge(ledge);

                    if (!blamo.HeightLevel() && !blamo.IsOnPlatform())
                    {
                        blamo.ChangeLevel();
                    }
                }
               
                LevelTrigger = false;
            }

            ActivateJumpPads();
        }

        if (other.tag == "HellReaper") 
        {
            Blamo blamo = FindObjectOfType<Blamo>();

            DeactivateJumpPads();

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

                DeactivateJumpPads();
            }
        }
    }

    private GameObject JumpPadChosen()
    {
        GameObject currentJumpPad = null;

        Blamo blamo = FindObjectOfType<Blamo>();

        for (int i = 0; i < jumpPads.Length; i++)
        {

            currentJumpPad = jumpPads[i];

            if (Mathf.Abs(blamo.transform.position.z - jumpPads[i].transform.localPosition.z) < 9) 
            {
                currentJumpPad = jumpPads[i];
            }
        }

        return currentJumpPad;
    }

    private void ActivateJumpPads()
    {
        for (int i = 0; i < jumpPads.Length; i++) 
        {
            jumpPads[i].SetActive(true);
        }
    }

    private void DeactivateJumpPads()
    {
        for (int i = 0; i < jumpPads.Length; i++)
        {
            jumpPads[i].SetActive(false);
        }
    }
}
