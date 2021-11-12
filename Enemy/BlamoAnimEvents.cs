using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlamoAnimEvents : MonoBehaviour
{
    public Blamo blamo;

    public GameObject hurtBox;
    private void Start()
    {
        blamo = GetComponentInParent<Blamo>();
    }
    public void Jump() 
    {
        blamo.Jump();
    }
    
    public void Attack() 
    {
        hurtBox.SetActive(true);
    }

    public void ResetAttack() 
    {
        hurtBox.SetActive(false);
    }
}
