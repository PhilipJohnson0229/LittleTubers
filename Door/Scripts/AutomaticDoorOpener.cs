using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorOpener : MonoBehaviour {
    [Tooltip("This script makes it so once the door opens, IT WILL NOT CLOSE. It stays open forever. If you'd like to have doors that close/open as you leave/enter, then " +
    "feel free to use the other script provided 'OpenAndClose'. Make sure the doors and triggers correspond. What that means is, if you place a door in element 0" +
    " of the 'AutomaticDoor' array, then the trigger for that door should be labeled 'AutoDoorTrigger0'. If you place a door" +
    " in element 23 of the 'AutomaticDoor' array, the trigger for that door should be labeled 'AutoDoorTrigger23'.")]
    public bool helper; //Set of explanations in editor, literally does nothing with the code, just here to give information about code. Just ignore this variable for right now.
    [Tooltip("Make sure the number of elements in 'Automatic Door' is the same as the number of elements in 'Already Opened' ")]
    public GameObject[] AutomaticDoor; //Array of Automatic Doors, the actual door object
    private string PreviouslyOpened; //String to hold the value of the door that was last opened
    [Tooltip("Make sure the number of elements in 'Automatic Door' is the same as the number of elements in 'Already Opened' ")]
    public bool[] alreadyOpened; //Array of booleans that make sure the open animation isnt played more than once
    [Tooltip("Place 'OpenDoorSound' in Open Door Sound.")]
    public AudioClip openDoorSound;//Audio Clips for the opening and closing sounds

    void Start () {
        PreviouslyOpened = ""; //Intializing the string "PreviouslyOpened" to an empty string
        PlayerPrefs.SetString("DoorOpened", "None"); //Setting the playerprefs string "DoorsOpened" (which is the key or ID) to a one time string, "None" (What the actual value of the string is)
        //*Note* The string "None" could be set to anything else you want, its just a place holder
	}

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < AutomaticDoor.Length; i++) //For loop going through the array of gameobjects 
        {
            if (other.gameObject.name == "AutoDoorTrigger" + i) // Sayin, "If player enters trigger named "AutoDoorTrigger#""
                //*Note*, the '#' in the last comment represents the number you put when you create the trigger in the actual editor
                //for instance, for the first door you make, create the door, and add the trigger named "AutoDoorTrigger0", for the
                //second door you make, you'll create the door, and add the trigger named "AutoDoorTrigger1" and so on
            {
                PlayerPrefs.SetString("DoorOpened", "AutoDoorTrigger" + i); //Set the playerprefs string to the trigger name you just entered, the i's correlate to the number at the end of the trigger name
                if (PlayerPrefs.GetString("DoorOpened") != PreviouslyOpened && alreadyOpened[i] == false) //Saying, "If this door is different from other doors AND this door is currently closed
                {
                    AutomaticDoor[i].GetComponent<Animation>().Play("OpenDoor"); //Play open door animation for this specific door
                    alreadyOpened[i] = true; //Makes this door remain opened, and make it so it cannot play the animation again
                    AutomaticDoor[i].GetComponent<AudioSource>().clip = openDoorSound; //set the audio source sound to 'openDoorSound'
                    AutomaticDoor[i].GetComponent<AudioSource>().Play(); //Play the audio when the door opens
                }
                PreviouslyOpened = PlayerPrefs.GetString("DoorOpened"); //Sets "DoorOpened" to the last door you opened
            }
        }
        }
}
