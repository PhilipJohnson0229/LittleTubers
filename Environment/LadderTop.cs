using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : MonoBehaviour
{
    [SerializeField]
    private Ladder _ladder;

    void OnTriggerEnter(Collider _other) 
    {
        if (_other.CompareTag("Player"))
        {
            Player _player = _other.GetComponent<Player>();
            if (_player != null) 
            {
                _player.LadderCheck(_ladder, true, true);
            }
        }
    }

    void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            Player _player = _other.GetComponent<Player>();
            if (_player != null)
            {
                _player.LadderCheck(null, false, false);
                _player.ClearLadderFlag();
            }
        }
    }
}
