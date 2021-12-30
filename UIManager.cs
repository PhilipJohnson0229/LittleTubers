using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //setting the instance with a property
    private static UIManager _instance;
    public static UIManager instance 
    {
        get 
        {
            if (_instance == null) 
            {
                Debug.LogError("There is no UI Manager");
            }
            return _instance; 
        }
    }

    [SerializeField]
    private Text _coinText, _notifText, _coinCount;

    public Image _selection, camFade, healthImage;

    public Sprite[] lives;

    public int levelToLoad;

    private void Awake()
    {
        _instance = this;
       
    }

    private void Start()
    {
        if (GameManager.instance._inHell)
        {
            healthImage.enabled = false;
            _coinText.enabled = true;
        }
    }

    public void UpdateCoins(int _coins) 
    {
        if (_coinText.isActiveAndEnabled) 
        {
            _coinText.text = "Coins: " + _coins.ToString();
        }
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
}
