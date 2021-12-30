using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour {
    [Tooltip("Make sure the doors and triggers correspond. What that means is, if you place a door in element 0" +
    " of the 'AutomaticDoor' array, then the trigger for that door should be labeled 'AutoDoorTrigger(0)'. If you place a door" +
        " in element 52 of the 'AutomaticDoor' array, the trigger for that door should be labeled 'AutoDoorTrigger(52)'. If you'd like to have the doors" +
        " stay open forever instead of open/close every time, then feel free to use the 'AutomaticDoorOpener' script. The reason for the parenthesis is so this script can" +
        " be ran in conjunction with 'AutomaticDoorOpener'. Put the audioclips that come with this package in their corresponding spots.")]
    public bool helper; //Set of explanations in editor, literally does nothing with the code, just here to give information about code. Just ignore this variable for right now.

    public GameObject[] AutomaticDoor; //Array of Automatic Doors, the actual door object
   [Tooltip("Place 'OpenDoorSound' in Open Door Sound and place 'CloseDoorSound' in Close Door Sound.")]
   public AudioClip openDoorSound, closeDoorSound; //Audio Clips for the opening and closing sounds


    //Open door part
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < AutomaticDoor.Length; i++) //For loop going through the array of gameobjects 
        {
            if (other.gameObject.name == "AutoDoorTrigger(" + i + ")") // Sayin, "If player enters trigger named "AutoDoorTrigger(#)""
                                                                //*Note*, the '#' in the last comment represents the number you put when you create the trigger in the actual editor
                                                                //for instance, for the first door you make, create the door, and add the trigger named "AutoDoorTrigger(0)", for the
                                                                //second door you make, you'll create the door, and add the trigger named "AutoDoorTrigger(1)" and so on
            {
                    AutomaticDoor[i].GetComponent<Animation>().Play("OpenDoor"); //Play open door animation for this specific door
                AutomaticDoor[i].GetComponent<AudioSource>().clip = openDoorSound; //set the audio source sound to 'openDoorSound'
                AutomaticDoor[i].GetComponent<AudioSource>().Play(); //Play the audio when the door opens

            }
        }
    }
    //Close door part
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < AutomaticDoor.Length; i++) 
        {
            if (other.gameObject.name == "AutoDoorTrigger(" + i +")") 
            {
                    AutomaticDoor[i].GetComponent<Animation>().Play("CloseDoor"); //Play close door animation for this specific door
                AutomaticDoor[i].GetComponent<AudioSource>().clip = closeDoorSound; //set the audio source sound to 'closeDoorSound'
                AutomaticDoor[i].GetComponent<AudioSource>().Play(); //Play the audio when the door closes
            }
        }
    }
}

