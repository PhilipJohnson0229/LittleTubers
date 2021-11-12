using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int _amount = 1;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Player _player = other.GetComponent<Player>();

            if (_player != null) 
            {
                _player.CollectCoin(_amount);
                Destroy(this.gameObject);
            }
        }
    }
}
