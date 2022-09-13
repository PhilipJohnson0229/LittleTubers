using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKey : MonoBehaviour
{
    public UIManager uiManager;

    public int levelToLoad;
    private void OnEnable()
    {
        uiManager.LoadNextLevel(levelToLoad);
    }
}
