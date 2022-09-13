using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxToken : MonoBehaviour
{
    public int songToPlay;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AudioManager.instance.PlayMusic(songToPlay);

        Destroy(gameObject, 5f);
    }

  
}
