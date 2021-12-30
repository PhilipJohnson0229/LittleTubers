using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniferKey : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Player player;
    public bool requiresTrigger = false;
    [SerializeField]
    private bool playerIsCLoseEnough = false, Triggered = false;
    private Animator anim;

    public GameObject lockedDoor;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        Keypad.incorrectPassword += TriggerAttack;
    }
    void Update()
    {
        float playerDistance = Mathf.Abs(transform.position.z - player.transform.position.z);
       
        if (playerDistance < 5)
        {
            playerIsCLoseEnough = true;
        }

        if (requiresTrigger)
        {
            if (Triggered)
            {
                anim.SetBool("Activate", true);
                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PFH_Run"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    if (lockedDoor != null)
                    {
                        lockedDoor.GetComponent<Animator>().SetBool("Open", true);
                    }
                }
            }
        }
        else
        {
            if (playerIsCLoseEnough)
            {
                anim.SetBool("Activate", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PFH_Run"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    if (lockedDoor != null)
                    {
                        lockedDoor.GetComponent<Animator>().SetBool("Open", true);
                    }
                }
            }
        }
    }

    public void TriggerAttack() 
    {
        Triggered = true;
    }
}
