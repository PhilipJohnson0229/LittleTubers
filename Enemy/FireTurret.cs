using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : MonoBehaviour
{
    [SerializeField]
    private Turret _turret;

    private void Start() 
    {
        _turret = GetComponentInParent<Turret>();
    }
    public void Fire() 
    {
        if (_turret != null) 
        {
            _turret.Attack();
        }
    }
}
