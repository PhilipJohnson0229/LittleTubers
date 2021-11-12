using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private bool _canHit = true;

    public int amount;

    void OnTriggerEnter(Collider _other)
    {
        IDamageable hit = _other.GetComponent<IDamageable>(); 
        if (hit != null) 
        {
            if (_canHit) 
            {
                hit.Damage(amount);

                _canHit = false;
                StartCoroutine(AttackCooldown());
            }  
        }
    }
    IEnumerator AttackCooldown() 
    {  
        yield return new WaitForSeconds(0.2f);
        
        _canHit = true;
    }
}
