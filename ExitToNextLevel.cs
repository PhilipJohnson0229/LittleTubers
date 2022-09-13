using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToNextLevel : MonoBehaviour
{
    public Image camFade;

    public Text hellText;

    public int selectedLevel, twistedLevel;

    public bool loadingIn = false, destroyAM = false;

    [SerializeField]
    private bool leavingHell = false, inHell = false;

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
            if (!loadingIn && !leavingHell)
            {
                StartCoroutine(FadeToNextLevel());
            }
            else if (!loadingIn && leavingHell) 
            {
                StartCoroutine(FadeToFinalLevel());
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

        if (destroyAM)
        {
            Destroy(AudioManager.instance.gameObject);
        }
    }

    IEnumerator FadeToFinalLevel()
    {
        float camFadeTarget = 1f;

        while (camFade.color.a < camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;


            yield return waitTime;
        }

        SceneManager.LoadScene(twistedLevel);        

        if (destroyAM)
        {
            Destroy(AudioManager.instance.gameObject);
        }
    }

    IEnumerator FadeIntoLevel()
    {
        float camFadeTarget = 0f;


        Player player = GameObject.FindObjectOfType<Player>();

        if (player != null)
        {
            player.transform.position = this.transform.position;
        }

        while (camFade.color.a > camFadeTarget)
        {
            var tempColor = camFade.color;
            
            tempColor.a -= .1f;
            camFade.color = tempColor;
            if (hellText != null) 
            {
                var tempHellTextColor = hellText.color;
                tempHellTextColor.a -= .1f;
                hellText.color = tempHellTextColor;
            }
            yield return waitTime;
        }

    }
}
