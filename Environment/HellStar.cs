using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HellStar : MonoBehaviour
{
    public Animator eyeOfJudgement;

    public event Action JudgementPassed;

    public PlayerData player;

    public GameObject gameOverCutscene;
  

    public void JudgeSoul() 
    {
        JudgementPassed?.Invoke();

        if (gameOverCutscene != null) 
        {
            gameOverCutscene.SetActive(true);
        }
    }

   
}
