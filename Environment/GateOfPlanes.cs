using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateOfPlanes : MonoBehaviour
{
    public int bossLevel, livingWorld;

    [SerializeField]
    AudioSource aSource;

    UIManager uiManager;
    public GameObject particleEffect;

    private void OnEnable()
    {
        aSource = GetComponent<AudioSource>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    public void LoadBossLevel()
    {
        if (uiManager != null) 
        {
            uiManager.LoadNextLevel(bossLevel);
        }
    }

    public void LoadLivingWorld() 
    {
        if (uiManager != null) 
        {
            uiManager.LoadNextLevel(livingWorld);
           
        }
    }

    public void PlaySound() 
    {
        aSource.Play();
    }
    // Update is called once per frame

    public void enableEffect() 
    {
        particleEffect.SetActive(true);
    }

    public void KillSelf() 
    {
        gameObject.SetActive(false);
    }
}
