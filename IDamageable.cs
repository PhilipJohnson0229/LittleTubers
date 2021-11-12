using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we will use this interface for all objects that can receive damage
//this is a contract that REQUIRES the inreiting class to implement these methods
public interface IDamageable 
{
    //we cant use variables or fields or instances in an interface
    //we have to use properties with getters and setters
    //the line below is called an auto property
    int health { get; set; }


    //there is not implementation in an interface we just define a methods and the inheriting class fills it with unique code
    void Damage(int amount);
}
