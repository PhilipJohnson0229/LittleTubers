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
            birdEnemy[birdIndex].SetActive(true);

            other.gameObject.SetActive(false);

            if (birdIndex == birdEnemy.Length - 1) 
            {
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }

            birdIndex++;
        }
    }


}
