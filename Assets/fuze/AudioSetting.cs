using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AudioSetting : MonoBehaviour
{
    AudioSource bgmSource;
    GameSoundController gameSoundContoroller;

    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI SEVolumeText;

    public static AudioSetting instance;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        var sources = GetComponentsInChildren<AudioSource>();
        bgmSource = sources[0];
        gameSoundContoroller = GetComponentInChildren<GameSoundController>();
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneLoaded (Scene nextScene, LoadSceneMode mode) {
        if(nextScene.name == "Title"){
            Slider[] sliders = FindObjectsOfType<Slider>(true);
            sliders[1].onValueChanged.AddListener(ChangeBGMVolume);
            sliders[0].onValueChanged.AddListener(ChangeGameSoundVolume);
        }
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
