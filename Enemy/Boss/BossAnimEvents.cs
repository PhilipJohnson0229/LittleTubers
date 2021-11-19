using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvents : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    public void InitiateBossBattle() 
    {
        BossPhase1Controller.instance.ExitIntro();
        BossPhase1Controller.instance.SetTarget();
    }
}
