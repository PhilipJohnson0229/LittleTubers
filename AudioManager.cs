using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instace;

    public AudioSource[] musicTracks;
    public AudioSource[] sfx;

    public int levelMusicToPlay;

    public AudioMixerGroup musicMixer, sfxMixer;
    //private int currentTrack;
    void Awake()
    {
        instace = this;
    }

    // Update is called once per frame
    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

                
            PlaySoundEffects(Random.Range(0, 9));
        }
    }

    public void PlayMusic(int musicToPlay)
    {
        for (int i =0; i < musicTracks.Length; i++)
        {
            musicTracks[i].Stop();
        }
        musicTracks[musicToPlay].Play();
    }

    public void PlaySoundEffects(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        // musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSLider.value);
    }

    public void SetSFXLevel()
    {
        //sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxVolSlider.value);
    }
}


