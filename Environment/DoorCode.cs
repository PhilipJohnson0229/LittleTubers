using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorCode : MonoBehaviour
{
    [SerializeField]
    private int code;

    public TMP_Text doorCode;
    // Start is called before the first frame update
    void Start()
    {
        code = Random.Range(100, 999);

        doorCode.text = code.ToString();

        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (cam != null) 
        {
            cam.SetCode(code);
        }
    }

    
}
