using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _yOffset = 1f;
    [SerializeField]
    private float _xOffset = 1f;
    [SerializeField]
    private float _smoothTime = 0.3F;

    public GameObject numPad;

    [SerializeField]
    private Keypad[] keypadButtons;

    // Start is called before the first frame update
    void Start()
    {  
        if (_target == null) 
        {
            Debug.Log("No target found for camera");
        }

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 _targetPosition = new Vector3(transform.position.x + _xOffset, _target.position.y + _yOffset, _target.position.z);
            // Smoothly move the camera towards that target position
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothTime);
        }
    }

    public void ActivateNumberPad()
    {
        numPad.SetActive(true);
        //keypadButtons = GameObject.FindObjectsOfType<Keypad>();
    }

    public void DeactivateNumberPad()
    {
        numPad.SetActive(false);
    }

    public void SetDoorToUnlock(GameObject doorToOpen) 
    {
        foreach (Keypad numButton in keypadButtons) 
        {
            numButton.DoorTrigger = doorToOpen;
        }
    }

    public void SetCode(int code)
    {
        foreach (Keypad numButton in keypadButtons)
        {
            numButton.SetPassword(code.ToString());
        }
    }
}
