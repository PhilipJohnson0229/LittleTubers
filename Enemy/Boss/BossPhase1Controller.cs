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

    public bool canBeHit = true, canMove = false;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    public Vector3 direction, playerDirection;

    public Transform trackedTarget, leftPillar, rightPillar, playerModel, player;

    public SkinnedMeshRenderer mr;

    public float targetDistance, playerDistance, h, speed;

    public GameObject phase2Boss;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        //AudioManager.instance.PlayMusic(bossMusic);
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
        
        playerDirection = transform.localPosition - player.localPosition;
        
        
        playerDistance = Mathf.Abs(playerDirection.z);


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

    public void ExitIntro()
    {
        currentPhase = BossPhase.Phase1;
        anim.SetBool("Phase1", true);
    }

    public void ChasePlayer(Vector3 direction) 
    {
        Vector3 facing = playerModel.localEulerAngles;

        if (trackedTarget != null)
        {
            
            transform.Translate(direction * Time.deltaTime);
        }

        if (trackedTarget == rightPillar)
        {
            Debug.Log("looking left");
            facing.y = 40f;
            playerModel.localEulerAngles = facing;
            
            h = 1f;

            //Vector3 _attackPosition = new Vector3(transform.position.x, transform.position.y, (transform.localPosition.z - _player.transform.localPosition.z));
        }
        else if (trackedTarget == leftPillar)
        {
            Debug.Log("looking right");
            facing.y = 140f;
            playerModel.localEulerAngles = facing;
            
            h = -1f;

        }
    }

    public void AttackLunge()
    {
        anim.SetBool("Attacking", true);
    }

    public void DamageBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);

        
        health--;
        if (currentPhase != BossPhase.End)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Blink(2.5f));
        }

        if (health <= 0)
        {
            health = 3;
            currentPhase++;
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

                speed *= 1.5f;

                break;
            case BossPhase.Phase3:
                anim.SetBool("Phase3", true);
                anim.SetBool("Phase2", false);

                speed *= 2f;
                break;
            case BossPhase.End:
                anim.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;

        }

        if (trackedTarget == leftPillar)
        {
            trackedTarget = rightPillar;
        } else if (trackedTarget == rightPillar)
        {
            trackedTarget = leftPillar;
        }
    }

    public void SetTarget() 
    {
        Debug.Log("set target");
        if (playerDistance < 0) 
        {
            trackedTarget = rightPillar;
            h = 1;
        }
        else if(playerDistance > 0)
        {
            trackedTarget = leftPillar;
            h = -1; 
        }
    }

    IEnumerator Blink(float time)
    {
        bool isBlinking = false;

        canBeHit = false;

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

        canBeHit = true;
    }

    IEnumerator EndBoss()
    {
        //AudioManager.instace.PlaySoundEffects(bossHit);
        //AudioManager.instace.PlaySoundEffects(bossDeathShout);
        yield return new WaitForSeconds(exitRevealTime);
        phase2Boss.SetActive(true);
        //victoryZone.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (canBeHit == true)
            {
                DamageBoss();
            }
        }

    }
}
