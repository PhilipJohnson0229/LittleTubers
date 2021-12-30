using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] musicTracks;
    public AudioSource[] sfx;
    

    public int levelMusicToPlay, jokeIndex, tutorialIndex, enemyVictoryIndex;

    public AudioMixerGroup musicMixer, sfxMixer;
    //private int currentTrack;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        PlayMusic(levelMusicToPlay);
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


