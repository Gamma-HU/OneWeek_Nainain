using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSetting : MonoBehaviour
{
    AudioSource bgmSource;
    GameSoundController gameSoundContoroller;

    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI SEVolumeText;

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
        bgmVolumeText.text = (value * 100).ToString("0.0");
    }
    public void ChangeGameSoundVolume(float value){
        gameSoundContoroller.volume = value;
        SEVolumeText.text = (value * 100).ToString("0.0");
    }
}
