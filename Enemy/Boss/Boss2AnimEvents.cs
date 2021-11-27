using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2AnimEvents : MonoBehaviour
{
    public void SendToHell()
    {
        BossPhase1Controller.instance.Swallow();
    }
}
