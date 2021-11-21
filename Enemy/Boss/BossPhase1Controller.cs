using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1Controller : MonoBehaviour
{
    public static BossPhase1Controller instance;

    public Animator anim;

    public GameObject victoryZone;

    public float exitRevealTime;

    public int health = 3;

    public enum BossPhase { Intro, Phase1, Phase2, Phase3, End };

    public BossPhase currentPhase = BossPhase.Intro;

    public bool canBeHit, isLunging = false, canMove = false, playerHurtMe = false, gameStarted = true;

    public int bossMusic, bossDeath, bossDeathShout, bossHit, crowIndex = 0;

    public Vector3 direction, playerDirection, leftPillarSetTrans, rightPillarSetTrans;

    public Transform trackedTarget, leftPillar, rightPillar, playerModel, player, crowSpawnPoint;

    public SkinnedMeshRenderer mr;

    public float targetDistance, playerDistance, h, speed, speedSetter;

    public GameObject phase2Boss, deathDUmmy, crowDummy;

    public GameObject crowToSpawn;


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        AudioManager.instance.PlayMusic(bossMusic);
        
    }

    void Update()
    {
        if (trackedTarget != null) 
        {
            direction = transform.localPosition - trackedTarget.localPosition;
            targetDistance = Mathf.Abs(direction.z);

            Vector3 velocity = new Vector3(0, 0, h) * speed;

            if (canMove)
            {
                ChasePlayer(velocity);
            }
        }

        if (player != null)
        {
            playerDirection = transform.localPosition - player.localPosition;


            playerDistance = playerDirection.z;
        }
       


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

    public void SetInitialPhase() 
    {
        if (gameStarted) 
        {
            currentPhase++;
            health = 1;
            CheckPhase();
            Debug.Log("set phase was called");
        }
        
    }

    public void SetPhase()
    {
        if (playerHurtMe) 
        {
            currentPhase++;
            health = 1;
            CheckPhase();
            Debug.Log("set phase was called");
        }
    }

    public void CheckPhase() 
    {
        switch (currentPhase)
        {
            case BossPhase.Intro:
                currentPhase++;
                CheckPhase();
                break;

            case BossPhase.Phase1:
                anim.SetBool("Phase1", true);
                break;

            case BossPhase.Phase2:
                anim.SetBool("Phase2", true);
                anim.SetBool("Phase1", false);
                speedSetter *= 1.1f;
                break;

            case BossPhase.Phase3:
                anim.SetBool("Phase3", true);
                anim.SetBool("Phase2", false);
                speedSetter *= 1.2f;
                break;

            case BossPhase.End:
                StartCoroutine(EndBoss());
                break;

        }
    }

    void ChasePlayer(Vector3 direction) 
    {
        Vector3 facing = playerModel.localEulerAngles;

        if (trackedTarget != null)
        {
            transform.Translate(direction * Time.deltaTime);
        }

        if (trackedTarget == rightPillar)
        {
            facing.y = 40f;
            playerModel.localEulerAngles = facing;
            
            h = 1f;

        
        }
        else if (trackedTarget == leftPillar)
        {
            
            facing.y = 140f;
            playerModel.localEulerAngles = facing;
            
            h = -1f;

        }

        if (isLunging)
        {
            speed = 5f;
        }
        else 
        {
            speed = speedSetter;
        }
      


        if (Mathf.Abs(playerDistance) < 4) 
        {
            AttackLunge();
        }
    }

    public void AttackLunge()
    {
        anim.SetBool("Attack", true);
    }

  

    //we will rely on the animation to subtract health since ontriggerenter sucks
    public void SubtractHealth() 
    {
        health--;

        if (health <= 0)
        {
            health = 1;
            canBeHit = true;
            anim.SetBool("CanBeHit", true);
            StartCoroutine(Blink(10f));
        }
    }

    public void DamageBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);
       
        //the hurt animation will manage the health decrement
        if (currentPhase != BossPhase.End)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Blink(2.5f));
        }

        CheckPhase();  
    }

    public void ChangeTarget() 
    {
        
        if (trackedTarget == leftPillar)
        {
            trackedTarget = rightPillar;
        }
        else if (trackedTarget == rightPillar)
        {
            trackedTarget = leftPillar;
        }

    }

    public void SetTarget() 
    {
        if (playerDistance < 0 && trackedTarget != rightPillar)
        {
            trackedTarget = rightPillar;

        }
        else if (playerDistance > 0 && trackedTarget != leftPillar)
        {
            trackedTarget = leftPillar;

        }

        if (playerHurtMe)
        {
            playerHurtMe = false;
        }
        
    }

    public void Kill()
    {
        deathDUmmy.SetActive(true);
        anim.SetBool("Kill", true);
    }

    public void Swallow()
    {
        deathDUmmy.SetActive(false);
        UIManager.instance.LoadNextLevel(0);
    }

    public void ReleaseTheCrow() 
    {
        crowToSpawn.transform.position = crowSpawnPoint.position;
        crowDummy.SetActive(false);
        crowToSpawn.SetActive(true);        
    }

    IEnumerator Blink(float time)
    {
        bool isBlinking = false;

        while (time > 0)
        {
            isBlinking = !isBlinking;

            if (isBlinking)
            {
                mr.materials[2].color = Color.red;

            }
            else
            {
                mr.materials[2].color = Color.green;

            }

            time -= .1f;

            yield return new WaitForSeconds(.05f);
        }

       
    }

    IEnumerator EndBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);
        //AudioManager.instace.PlaySoundEffects(bossDeathShout);
        Debug.Log("end boss was called successfully");
        canMove = false;
        canBeHit = false;
        anim.SetBool("isDead", true);
        anim.SetBool("Phase3", false);
        yield return new WaitForSeconds(exitRevealTime);
        //crowToSpawn.SetActive(false);
        phase2Boss.SetActive(true);
        Destroy(this.gameObject);
        //victoryZone.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canBeHit) 
        {
            if (other.tag == "Left Wall")
            {
                DamageBoss();
                transform.position = leftPillarSetTrans;

            }
            else if (other.tag == "Right Wall")
            {
                DamageBoss();
                transform.position = rightPillarSetTrans;
            }
        }
    }
}
