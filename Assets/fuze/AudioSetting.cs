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
        bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        gameSoundContoroller.volume = PlayerPrefs.GetFloat("SEVolume", 1.0f);
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
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
    public void ChangeGameSoundVolume(float value){
        gameSoundContoroller.volume = value;
        SEVolumeText.text = (value * 100).ToString("0.0");
        PlayerPrefs.SetFloat("SEVolume", value);
    }

    public void ResetVolume()
    {
        bgmSource.volume = 1.0f;
        gameSoundContoroller.volume = 1.0f;
        bgmVolumeText.text = (100.0f).ToString("0.0");
        SEVolumeText.text = (100.0f).ToString("0.0");
        Slider[] sliders = FindObjectsOfType<Slider>(true);
        sliders[1].value = 1.0f;
        sliders[0].value = 1.0f;
    }
}
