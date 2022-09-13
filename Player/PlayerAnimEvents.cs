using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    int footSteps;

    public Player player;
    public void Footsteps() 
    {
        AudioManager.instance.PlaySoundEffects(footSteps);
    }

    public void UnStun() 
    {
        player.UnStun();
    }
}
