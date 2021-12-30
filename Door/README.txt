Features and Instructions
-------------------------

- A door with multiple pieces (fbx and prefab), a keypad, interchangable shaders/materials, scripts, animations, and audio.
- Doors are automatic, they either can stay open forever or open/close when you enter/leave, or can be opened with a 3D Keypad.
-Uses triggers to open the doors, once the player enters a specific trigger, the door will open.
-Once the player leaves the trigger, the door will close*
*Only if you're using OpenAndClose.cs
-Enabling the 'Helper' boolean wont do anything.
-These scripts can be used in conjunction with eachother,
meaning that you can have one door that opens/closes and
have another door that stays open.
-It does not matter what the doors are labeled, you can leave every door labeled 'Door' of you'd like,
or change it to whatever, it doesnt matter. THE IMPORTANT PART IS THAT THE TRIGGER #'S AND ARRAY INDEX MATCH!
-The Keypad is set to use either the OpenAndClose.cs or AutomaticDoorOpener.cs
-The Keypad has a 4-digit code you can set yourself

How to Use (Automatic Door Opener)
------------

-Add this to your player gameObject
-This is the script that allows the door to opened once, and stay open forever.
-There is an array of Doors labeled 'Automatic Door'. Put each door (each door you'd like to remain open once it
has been opened) in each spot. So if you have two doors you'd like to open, and stay open, then make the size of 
the array 2, and put a door in each element.
-There will be an array of booleans called 'Already Opened'. Make sure this array's size is the SAME as
the size of the 'Automatic Door' array.
-To make these doors work, the triggers have to be labeled correctly. Add triggers in front of/around each door.
-Each trigger should be labeled 'AutoDoorTrigger#'. The # should be the element index of the automatic door array that you'd want to open.
-To elaborate more on the trigger situation, if you have a door in element 0 of the Automatic Door array,
then the trigger to open that door should be labeled 'AutoDoorTrigger0'. If you were to have a door in element 4
of the Automatic Door array, the trigger to open that door should be labeled 'AutoDoorTrigger4'. 
-In the 'Open Door Sound' space, put the 'OpenDoorSound' (or any sound you want) in that empty space.
-Hover over the helper boolean (to reveal the ToolTip) for help too!

How to Use (Open and Close)
------------

-Add this to your player gameObject
-This is the script that allows the door to opened and closed when youre a certain distance.
-There is an array of Doors labeled 'Automatic Door'. Put each door (each door you'd like to open and close) 
in each spot. So if you have two doors you'd like to open and close, then make the size of 
the array 2, and put a door in each element.
-To make these doors work, the triggers have to be labeled correctly. Add triggers in front of/around each door.
-Each trigger should be labeled 'AutoDoorTrigger(#)'. The # should be the element index of the automatic door array that you'd want to open.
-To elaborate more on the trigger situation, if you have a door in element 0 of the Automatic Door array,
then the trigger to open that door should be labeled 'AutoDoorTrigger(0)'. If you were to have a door in element 4
of the Automatic Door array, the trigger to open that door should be labeled 'AutoDoorTrigger(4)'. 
-ITS VERY IMPORTANT THAT THE TRIGGERS HAVE THE PARENTHESIS IN ORDER FOR UNITY TO TELL THE DIFFERENCE 
BETWEEN THE OPEN/CLOSE DOORS AND THE PERMAOPEN DOORS.
-In the 'Open Door Sound' space, put the 'OpenDoorSound' (or any sound you want) in that empty space.
-In the 'Close Door Sound' space, put the 'CloseDoorSound' (or any sound you want) in that empty space.
-Hover over the helper boolean (to reveal the ToolTip) for help too!

How to Use (Keypad)
------------

-Add the 'Numpad' Prefab from the 'Prefab' Folder included in the 'Numpad' Folder wherever you would like. (Typically adjacent to the door you'd like to open)
-Highlight ever number in the 'Hierarchy' under 'Cube' and place the door trigger of the corresponding door in the 'Door Trigger' field in the inspector.
-With all the numbers still highlighted, feel free to change the 'Password' field to any 4-digit numerical code of your liking.
-For the best usage, I would put the door trigger in front of the keypad so as soon as the user enters the right code it would open the door as soon as it is entered.*
*To be more clear, putting the selected door trigger by the keypad gives it the illusion the keypad just opened the door as soon as you enter the right numbers, 
however, if you'd like to make it look like the door has been unlocked, I would leave it in front of the door so the player can walk up to it and it opens.

Support
-------

If you are having issues, please let me know.
Email me @ waggonerjake99@gmail.com for quesitons and feeback.
Please make sure to put a subject in the email too!
I will try to answer as fast as I can.