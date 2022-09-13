using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance 
    {
        get 
        {
            if (_instance == null)
            {
                Debug.LogError("Game manager is null");
            }
            return _instance;
        }
    }

    public GameObject[] activators;

    public int chosenActivator;
    //auto property
    public bool _hasKeyToCastle { get; set; }

    public bool _gameHasStarted { get; set; }

    public bool _inHell { get; set; }

    private void Awake()
    {
        _instance = this;
    }

  

}
