using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvents : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    public void PhaseCheck() 
    {
        BossPhase1Controller.instance.SetPhase();
    }

    public void SubtractHealth() 
    {
        BossPhase1Controller.instance.SubtractHealth();
    }

    public void SetTarget() 
    {
        BossPhase1Controller.instance.SetTarget();  
    }

    public void CheckTarget() 
    {
        BossPhase1Controller.instance.ChangeTarget();
    }
}
