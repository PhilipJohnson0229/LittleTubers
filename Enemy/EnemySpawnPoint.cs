using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject key, activator, enemyPrefab, cutscene;
    [SerializeField]
    private GameObject currentEnemy;
    public Transform pointA, pointB;
    public bool triggered = false, enemyDefeated = false, spawned = false, hasCutscene = false, isBird = false;
    private Vector3 keyOrigin;


    private void OnEnable()
    {
        Keypad.incorrectPassword += SpawnEnemy;

        GiantBird.birdDefeated += EnemyDefeated;
    }

    private void Start()
    {
        keyOrigin = this.transform.position;  
    }
    public void SpawnEnemy() 
    {
        Debug.Log("Spawn Enemy called");
        key.transform.position = keyOrigin;
        key.SetActive(true);
        activator.SetActive(true);

        if (isBird) 
        {
            currentEnemy.GetComponentInChildren<Animator>().ResetTrigger("Death");
            currentEnemy.GetComponentInChildren<Animator>().Play("OwlRun");
        }
        

        Rigidbody rb = currentEnemy.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.velocity = Vector3.zero;
        
    }

    public void VerifyChain()
    {
        if (enemyDefeated)
        {
            return;  
        }
        StartCoroutine(Verification());
    }

    private WaitForSeconds cooldown = new WaitForSeconds(3f);

    IEnumerator Verification()
    { 
        yield return cooldown;

        SpawnEnemy();
    }

    public void EnemyDefeated() 
    {
        enemyDefeated = true;
    }


    //it is paramount to unsubscribe and subscribe events in each script that uses them to prevent weird errors
    private void OnDisable()
    {
        Keypad.incorrectPassword -= SpawnEnemy;

        GiantBird.birdDefeated -= EnemyDefeated;
    }
}
