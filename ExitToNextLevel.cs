using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ExitToNextLevel : MonoBehaviour
{
    public Image camFade;

    public int selectedLevel;

    public bool loadingIn = false;

    private void Start()
    {
        if (loadingIn)
        {
            
            StartCoroutine(FadeIntoLevel());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            if(!loadingIn) 
            {
                StartCoroutine(FadeToNextLevel());
            }
        }   
    }
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    IEnumerator FadeToNextLevel()
    {
        float camFadeTarget = 1f;
        while (camFade.color.a < camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;


            yield return waitTime;
        }

        SceneManager.LoadScene(selectedLevel);
    }

   
    IEnumerator FadeIntoLevel()
    {
        float camFadeTarget = 0f;


        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (player != null)
        {
            player.transform.position = this.transform.position;
        }

        while (camFade.color.a > camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a -= .1f;
            camFade.color = tempColor;

            yield return waitTime;
        }

    }
}
