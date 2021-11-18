﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Animator anim;

    public GameObject victoryZone;

    public float exitRevealTime;

    public int health = 3;

    public enum BossPhase { Intro, Phase1, Phase2, Phase3, End };

    public BossPhase currentPhase = BossPhase.Intro;

    public bool canBeHit = true;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    public SkinnedMeshRenderer mr;

    public Light lightBulb;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        AudioManager.instace.PlayMusic(bossMusic);
        lightBulb.color = Color.green;
    }

    void Update()
    {
        /*if (GameManager.instance.isRespawning)
        {
            currentPhase = BossPhase.Intro;

            anim.SetBool("Phase1", false);
            anim.SetBool("Phase2", false);
            anim.SetBool("Phase3", false);

            //AudioManager.instace.PlayMusic(AudioManager.instace.levelMusicToPlay);

            gameObject.SetActive(false);

            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(true);

            //GameManager.instance.isRespawning = false;
        }*/
    }




    public void DamageBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);

        currentPhase++;
        health--;
        if (currentPhase != BossPhase.End)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Blink(2.5f));
        }

        if (health <= 0)
        {
            health = 0;
            currentPhase = BossPhase.End;
            StartCoroutine(Blink(10f));
        }
        switch (currentPhase)
        {
            case BossPhase.Phase1:
                anim.SetBool("Phase1", true);
                break;
            case BossPhase.Phase2:
                anim.SetBool("Phase2", true);
                anim.SetBool("Phase1", false);
                break;
            case BossPhase.Phase3:
                anim.SetBool("Phase3", true);
                anim.SetBool("Phase2", false);
                break;
            case BossPhase.End:
                anim.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;

        }

        IEnumerator Blink(float time) 
        {
            bool isBlinking = false;

            canBeHit = false;

            while(time > 0)
            {
                isBlinking = !isBlinking;

                if (isBlinking)
                {
                    mr.materials[2].color = Color.red;
                    lightBulb.color = Color.red;
                }
                else 
                {
                    mr.materials[2].color = Color.green;
                    lightBulb.color = Color.green;
                }
                
                time-= .1f;

                yield return new WaitForSeconds(.05f);
            }

            canBeHit = true;
        }

        IEnumerator EndBoss()
        {
            //AudioManager.instace.PlaySoundEffects(bossHit);
            //AudioManager.instace.PlaySoundEffects(bossDeathShout);
            AudioManager.instace.PlayMusic(AudioManager.instace.levelMusicToPlay);
            yield return new WaitForSeconds(exitRevealTime);
            //victoryZone.SetActive(true);
        }
    }
}