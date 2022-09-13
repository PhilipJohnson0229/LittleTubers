using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SkipCutscene : MonoBehaviour
{
    public float skipTimeMark;

    [SerializeField]
    private PlayableDirector cutscene;

    private void OnEnable()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cutscene.time = skipTimeMark;
        }
    }
}
