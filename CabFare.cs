using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabFare : MonoBehaviour
{
    public int sceneToLoad;

    private void OnEnable()
    {
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();

        if (uiManager != null) 
        {
            uiManager.LoadNextLevel(sceneToLoad);
        }
    }
}
