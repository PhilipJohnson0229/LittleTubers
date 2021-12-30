using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButton;
    private int _requiredCoins = 6;
    [SerializeField]
    private UIManager _uIManager;

    public string _message;
    [SerializeField]
    private bool _wasMessageDisplayed = false;
    [SerializeField]
    private bool _wasButtonPressed = false;

    [SerializeField]
    private Elevator _elevator;

    public GameObject lever;

    void Start() 
    {
        _uIManager = FindObjectOfType<UIManager>();
        //_elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
    }
    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();

            if (Input.GetKeyUp(KeyCode.E))
            {
                lever.GetComponent<Animator>().SetTrigger("Called");
                if (_player != null) 
                {
                    _player.Freeze();
                }
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _wasMessageDisplayed = false;
            _uIManager.Notification("");
        }
    }
}
