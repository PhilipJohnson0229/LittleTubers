using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour {
    [SerializeField]
    private string password = "000"; //This is the code for the keypad, change this to whatever you want it to be.
    public TextMesh code; //Is the numbers that appear on the keypad at the top.
    public GameObject DoorTrigger, enemyTrigger; //The door trigger you want to spawn so the door can be opened.
    AudioSource buttonSound; //Audio Source for the numpad to play sounds
    public int correctSound, incorrectSound;

    public delegate void onPasswordEntered();
    public delegate void onPasswordFailed();
    public static onPasswordEntered correctPassword;
    public static onPasswordFailed incorrectPassword;

    private void OnEnable()
    {
        correctPassword += RevealPath;
    }

    void Start () {
        code.text = ""; //Set the 'Insturctions' game object to an empty string.
        buttonSound = GetComponentInParent<AudioSource>() as AudioSource; //Find the auto source attached to the keypad   
    }
	
	void CheckAnswer () {
        if(code.text.Length > 2 && code.text != password) //Check if the code they entered is above 3 characters, but does not match the code you set.
        {
            code.text = ""; //Set the numbers on top back to a blank string.
            AudioManager.instance.PlaySoundEffects(incorrectSound);
            incorrectPassword();
        }
        if(code.text.Length >= 3 && code.text == password) //Check if the code they entered is greater than or equal to 4, and if it matches the code you set.
        {
            AudioManager.instance.PlaySoundEffects(correctSound);
            correctPassword();
        }
    }

    private void OnMouseDown()
    {
        code.text = code.text + gameObject.name; //Add the numbers to they enter to the top of the keypad.
        buttonSound.Play(); //Play the sound.
        CheckAnswer();
    }

    public void RevealPath() 
    {
        if (DoorTrigger != null) 
        {
            Animator doorAnim = DoorTrigger.GetComponent<Animator>();

            doorAnim.SetBool("Open", true);
        }

        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (mainCam != null) 
        {
            mainCam.DeactivateNumberPad();
        }
    }


    public void SetPassword(string _password) 
    {
        password = _password;
    }

    void OnDisable()
    {
        correctPassword -= RevealPath;
    }
}