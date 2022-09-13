using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    // Start is called before the first frame update
    public Blamo blamo;

    public BlamosLedge ledge;
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        blamo = other.GetComponent<Blamo>();

        if (blamo != null && blamo.GetMovementState())
        {
            blamo.AssignLedge(ledge);
            blamo.JumpForLedgeFromPad();
        }
        else { return; }
    }
}
