using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenRoad : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeed = 0.5f, iRingSpeed, oRingSpeed;

    public Material mat;

    public UIManager uiManager;

    public GameObject cutscene, innerRing, outerRing;

    private bool isRunning = true;

    float offset;

    void Update()
    {
        if (isRunning)
        {
            offset = Time.time * scrollSpeed;
            mat.SetTextureOffset("_MainTex", new Vector2(0, offset));

            innerRing.transform.Rotate(Vector3.up * iRingSpeed * Time.deltaTime);
            outerRing.transform.Rotate(Vector3.up * oRingSpeed * Time.deltaTime);
        }
      
        uiManager.data.CountDown();

        uiManager.slider.value = uiManager.data.getGirneyTime();

        if (uiManager.data.getGirneyTime() <= 0) 
        {
            isRunning = false;
        }
    }

    public void Stop() 
    {
        uiManager.Notification("Destination Reached");
        cutscene.SetActive(true);
    }

    void OnEnable() 
    {
        uiManager.data.destinationReached += Stop;
    }

    void OnDisable()
    {
        uiManager.data.destinationReached -= Stop;
    }
}
