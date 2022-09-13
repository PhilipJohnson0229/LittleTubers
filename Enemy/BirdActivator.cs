using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdActivator : MonoBehaviour
{
    public GameObject[] birdEnemy;
    public int birdIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BirdKey") 
        {
            other.gameObject.SetActive(false);
            birdEnemy[birdIndex].SetActive(true);

            birdEnemy[birdIndex].transform.position = this.transform.position;

            Enemy enemy = birdEnemy[birdIndex].GetComponent<Enemy>();

            if (enemy != null) 
            {
                enemy.Revive();
            }

            if (birdEnemy.Length > 1) 
            {
                birdIndex++;
            } 
        }
    }


}
