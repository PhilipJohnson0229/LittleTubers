using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{

    UIManager uiManager;

    public int fee;

    private GOP gop;

    private void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gop = GameObject.FindObjectOfType<GOP>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)) 
        {
            if (player != null) 
            {
                uiManager.ReapersReward();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (player != null)
            {
                uiManager.LeaveReaper();

            }
        }
    }

    public void Checkout() 
    {
        if (uiManager.data.getCoins() >= fee)
        {
            uiManager.data.PayToll();


            gop.unlocked = true;


            uiManager.LeaveReaper();
        }
        else 
        {
            uiManager.LeaveReaper();
        }
    }
}
