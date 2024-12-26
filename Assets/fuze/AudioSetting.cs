using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    AudioSource bgmSource;
    GameSoundController gameSoundContoroller;

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        var sources = GetComponentsInChildren<AudioSource>();
        bgmSource = sources[0];
        gameSoundContoroller = GetComponentInChildren<GameSoundController>();
    }

    public void ChangeBGMVolume(float value){
        bgmSource.volume = value;
    }
    public void ChangeGameSoundVolume(float value){
        gameSoundContoroller.volume = value;
    }
}
