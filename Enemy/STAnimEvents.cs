using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STAnimEvents : MonoBehaviour
{
    public SharkTrap st;
    [SerializeField]
    private Transform rotator;
    [SerializeField]
    Vector3 facing;
    [SerializeField]
    bool hasSwitched = false;
    [SerializeField]
    AudioSource aSource;

    private void OnEnable()
    {

        rotator = transform.parent;
        aSource = GetComponent<AudioSource>();
    }

    public void Switch() 
    {
        Debug.Log("calling switch");
        if (hasSwitched == false)
        {
            hasSwitched = true;
        }
        else 
        {
            hasSwitched = false;
        }

        if (st.PlayerWithinRange()) 
        {
            facing = rotator.eulerAngles;

            if (hasSwitched)
            {
                facing.y = 180;

                rotator.eulerAngles = facing;
            }
            else 
            {
                facing.y = 0;

                rotator.eulerAngles = facing;
            }
        }
    }


    public void PlaySound() 
    {
        aSource.Play();
    }
}
