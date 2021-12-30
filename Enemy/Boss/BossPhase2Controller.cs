using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Controller : MonoBehaviour
{
    public static BossPhase2Controller instance;

    public Animator anim;

    public GameObject victoryZone, playerCaughtScene;

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
        AudioManager.instance.PlayMusic(bossMusic);
        lightBulb.color = Color.green;
    }

    void Update()
    {
        /*if (GameManager.instance)
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

    public void Kill()
    {
        playerCaughtScene.SetActive(true);
    }

    public void Swallow()
    {
      
        UIManager.instance.LoadNextLevel(0);
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
    }

    private WaitForSeconds blinkTimer = new WaitForSeconds(.05f);

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

            yield return blinkTimer;
        }

        canBeHit = true;
    }
    
    private WaitForSeconds exitTimer = new WaitForSeconds(0.05f);

    IEnumerator EndBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);
        //AudioManager.instace.PlaySoundEffects(bossDeathShout);
        GiantBird[] crows = FindObjectsOfType<GiantBird>();
        foreach (GiantBird crow in crows) 
        {
            crow.Damage(1);
        }
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        yield return exitTimer;
        //victoryZone.SetActive(true);
    }
}
