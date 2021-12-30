using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject key, activator, enemyPrefab;
    private bool triggered = false;
    private Vector3 keyOrigin;
    private void Start()
    {
        Keypad.incorrectPassword += SpawnEnemy;
       
        keyOrigin = this.transform.position;
    }
    public void SpawnEnemy() 
    {
        key.transform.position = keyOrigin;
        key.SetActive(true);
        activator.SetActive(true);
        GameObject newEnemy = Instantiate(enemyPrefab, activator.transform.position, Quaternion.Euler(new Vector3(0,90f,0)));
        newEnemy.SetActive(false);

        BirdActivator theActivator = activator.GetComponent<BirdActivator>();
        theActivator.birdIndex = 0;
        theActivator.birdEnemy[0] = newEnemy;
    }
}
