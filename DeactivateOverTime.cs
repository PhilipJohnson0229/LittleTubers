using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOverTime : MonoBehaviour
{
    public float timeToWait;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(WaitToDeactivate(timeToWait));
    }


    private WaitForSeconds waitTimne = new WaitForSeconds(.1f);

    IEnumerator WaitToDeactivate(float time)
    {
        float waitTime = time;
        while (waitTime < 0)
        {
           yield return waitTime;
        }
       
        gameObject.SetActive(false);
    }
}
