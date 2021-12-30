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
    private void Start()
    {
        selectionUpdated += UpdateSelection;
        characterSelected += StartLoadingRoutine;
        selectionUpdated(selectionIndex);
    }
    // Update is called once per frame
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
            //this is actually a delegate im just practicing
            
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

        float camFadeTarget = 1f;
        while (camFade.color.a < camFadeTarget)
        {
            var tempColor = camFade.color;
            tempColor.a += .1f;
            camFade.color = tempColor;


            yield return waitTime;
        }

        SceneManager.LoadScene(selectedLevel[selectionIndex]);
    }

}
