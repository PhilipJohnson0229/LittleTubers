using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoAnimEvents : MonoBehaviour
{
    [SerializeField]
    private Blamo blamo;

    [SerializeField]
    private RushingBlamo rBlamo;

    [SerializeField]
    private float jumpPower = 6f;

    public GameObject hurtBox;

    public bool isHunter = true;
    
    [SerializeField]
    int footSteps, bonesMin, bonesMax, jump, land;
    private void Start()
    {
        if (isHunter)
        {
            blamo = GetComponentInParent<Blamo>();
        }
        else 
        {
            rBlamo = GetComponentInParent<RushingBlamo>();
        }  
    }
    public void Jump() 
    {
        blamo.Jump(jump, jumpPower);
    }

    public void PlayJumpSound() 
    {
        AudioManager.instance.PlaySoundEffects(jump);
    }

    public void PlayLandSound() 
    {
        AudioManager.instance.PlaySoundEffects(land);
    }

    public void Attack() 
    {
        hurtBox.SetActive(true);
    }

    public void ResetAttack() 
    {
        hurtBox.SetActive(false);
    }

    public void SendToHell() 
    {
        if (isHunter)
        {
            blamo.Swallow();
        }
        else
        {
            rBlamo.Swallow();
        }
    }

    public void Footsteps()
    {
        AudioManager.instance.PlaySoundEffects(footSteps);
    }

    public void BonesCracking()
    {
        int choice = Random.Range(bonesMin,bonesMax);
        AudioManager.instance.PlaySoundEffects(choice);
    }
}
