using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] selectionObjects;
    public string[] titles;
    public string[] selectedLevel;
    public GameObject selectionObject;
    [SerializeField]
    private int selectionIndex;
    [SerializeField]
    private bool characterChosen = false;
    [SerializeField]
    private Animator slectedObjAnim;
    public Image camFade;
    public Text characterName;

    public delegate void onSelectedEvent(int index);
    public static onSelectedEvent selectionUpdated;

    public delegate void onCharacterSelected();
    public static onCharacterSelected characterSelected;

    public PlayableDirector introCutscene;

    public PlayerData playerData;

    private void OnEnable()
    {
        selectionUpdated += UpdateSelection;
        characterSelected += StartLoadingRoutine;
    }

    private void Start()
    {
        selectionUpdated(selectionIndex);
    }
 
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            introCutscene.time = 5.0f;
        }

        if (!characterChosen && introCutscene.time > 4.5f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectionIndex++;
                if (selectionIndex > selectionObjects.Length - 1)
                {
                    selectionIndex = 0;
                }

                selectionUpdated(selectionIndex);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectionIndex--;
                if (selectionIndex < 0)
                {
                    selectionIndex = selectionObjects.Length - 1;
                }

                selectionUpdated(selectionIndex);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                characterChosen = true;
                speak();
                //StartCoroutine(FadeToNextLevel());
            }
        }
    }

    public void UpdateSelection(int index)
    {
        foreach (GameObject playerSelection in selectionObjects)
        {
            playerSelection.SetActive(false);
        }

        selectionObjects[index].SetActive(true);
        slectedObjAnim = selectionObjects[selectionIndex].GetComponentInChildren<Animator>();
        characterName.text = titles[index];

        AudioManager.instance.PlaySoundEffects(3);
    }

    public void speak()
    {
        if (slectedObjAnim.GetBool("Speak") == false)
        {
            slectedObjAnim.SetBool("Speak", true);

            AudioManager.instance.PlaySoundEffects(selectionIndex);
        }
        else { return; }
    }

    public void StartLoadingRoutine()
    {
        StartCoroutine(FadeToNextLevel());
    }

    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);

    IEnumerator FadeToNextLevel()
    {
        InitializePlayerData(playerData);

        float camFadeTarget = 1.5f;

        while (camFade.color.a < camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;

            characterName.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 5f);
            yield return waitTime;
        }

        Destroy(AudioManager.instance.gameObject, 1f);
        SceneManager.LoadScene(selectedLevel[selectionIndex]);
    }

    private void OnDisable()
    {
        selectionUpdated -= UpdateSelection;
        characterSelected -= StartLoadingRoutine;
    }

    private void InitializePlayerData(PlayerData playerData) 
    {
        playerData.setHealth(6);
        playerData.setCoins(0);
        playerData.setGirneyTime(40f);
        playerData.setInHell(false);
        playerData.setDied(false);
    }
}
