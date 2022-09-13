using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _notifText;

    [SerializeField]
    private int coins;

    public TMP_Text coinsText;

    public Image _selection, camFade, healthImage;

    public Sprite[] lives;

    public int levelToLoad, hellLevel;

    public PlayerData data;

    public Slider slider;

    public GameObject offering, deathText, notEnoughBlood, reapersOffer;

    [SerializeField]
    HellStar hellStar;

    bool enoughBlood = false;

    DeadZone deadZone;

    //we must subscribe and unsubscribe accordingliy when dealing with events or we will have data leaks
    //data leaks produce weird missingreferenc
    private void OnDisable()
    {
        data.changed -= UpdateCoins;
        data.destinationReached -= TurnOffSlider;
        data.ressurrected -= ReadCoin;
        if (hellStar != null)
        {
            hellStar.JudgementPassed -= MessageGameOver;

        }
    }

    private void OnEnable()
    {
        data.changed += UpdateCoins;
        data.destinationReached += TurnOffSlider;
        data.ressurrected += ReadCoin;
        if (hellStar != null) 
        {
            hellStar.JudgementPassed += MessageGameOver;

        }
    }


    private void Start()
    {
        coins = data.getCoins();
        coinsText.text = coins.ToString();

        deathText.SetActive(false);
        offering.SetActive(false);
        //notEnoughBlood.SetActive(false);

        deadZone = FindObjectOfType<DeadZone>();

        if (data.isInHell() == true) 
        {
            hellStar = GameObject.FindObjectOfType<HellStar>();
        }
    }

    public void UpdateCoins()
    {
        coins += 1;
        coinsText.text = coins.ToString();

    }

    public void ReadCoin() 
    {
        coins = data.getCoins();
        coinsText.text = coins.ToString();
        
    }

    public void UpdateLives(int _lives)
    {
        if (healthImage.isActiveAndEnabled)
        {
            healthImage.sprite = lives[_lives];
        }
    }

    public void Notification(string _message)
    {
        StartCoroutine(NotifyPlayer(_message));
    }

    public void UpdateSelection(int _yPos)
    {
        _selection.rectTransform.anchoredPosition = new Vector2(_selection.rectTransform.anchoredPosition.x, _yPos);
    }

    public void LoadNextLevel(int level)
    {
        StartCoroutine(FadeToNextLevel(level));
    }

    public void OfferAChoice()
    {
        StartCoroutine(FadeIntoDeath());

        deathText.SetActive(true);

        offering.SetActive(true);
    }

    public void CheckLifeOffering()
    {
        if (data.ReturnHealth() > 2)
        {
            deathText.SetActive(false);
            offering.SetActive(false);
            data.Damage(2);
            UpdateLives(data.ReturnHealth());
            DeadZone deadZone = FindObjectOfType<DeadZone>();

            if (deadZone != null) 
            {
                deadZone.TradeLifeForRespawn();
                StartCoroutine(FadeIntoLife());
            }
        }
        else if(data.ReturnHealth() <= 2)
        {
            
            deathText.SetActive(false);
            offering.SetActive(false);
            notEnoughBlood.SetActive(true);

            if (camFade.color.a == 0)
            {
                LoadNextLevel(8);
            }
            else 
            {
                SceneManager.LoadScene(hellLevel);
            }


            data.setInHell(true);
        }
    }

    public void HellSpawn() 
    {
        StartCoroutine(StallSpawn());
    }

    public void QuitToMenu()
    {
        Destroy(AudioManager.instance.gameObject);
        LoadNextLevel(0);
    }

    public void ReapersReward()
    {
        if (reapersOffer != null) 
        {
            reapersOffer.SetActive(true);
        }
        
    }

    public void LeaveReaper() 
    {
        if (reapersOffer != null)
        {
            reapersOffer.SetActive(false);
        }

    }

    IEnumerator NotifyPlayer(string _message)
    {

        if(_notifText != null) 
        {
            _notifText.text = _message.ToString();
            yield return new WaitForSeconds(1.5f);
            _notifText.text = "";
        }    
    }

    private WaitForSeconds fadeTime = new WaitForSeconds(.1f);

    IEnumerator FadeToNextLevel(int level) 
    {
        float camFadeTarget = 1f;
        while (camFade.color.a < camFadeTarget) 
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;


            yield return fadeTime;
        }

        SceneManager.LoadScene(level);
    }

    IEnumerator FadeIntoDeath()
    {
        float camFadeTarget = 1f;
        while (camFade.color.a < camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;


            yield return fadeTime;
        }
    }

    IEnumerator FadeIntoLife()
    {
        float camFadeTarget = 0f;

        while (camFade.color.a > camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a -= .1f;
            camFade.color = tempColor;


            yield return fadeTime;
        }
    }

    IEnumerator StallSpawn() 
    {
        yield return new WaitForSeconds(1f);

        deadZone.TradeLifeForRespawn();
    }

    public void TurnOffSlider()
    {
        slider.gameObject.SetActive(false);
    }

    public void MessageGameOver() 
    {
        Notification("Game Over");
    }
}
