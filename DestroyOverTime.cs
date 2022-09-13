using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public bool hasDeathEffect = false;
    public GameObject deathEffect;
    void OnEnable()
    {
        Destroy(this.gameObject, 1.5f);
        if (hasDeathEffect)
        {
            StartCoroutine(TurnOnEffect());
        }
    }

    void SpawnEffect() 
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    //caching
    private WaitForSeconds waitTime = new WaitForSeconds(2f);
    IEnumerator TurnOnEffect() 
    {

        yield return waitTime;

        SpawnEffect();
    }

}
