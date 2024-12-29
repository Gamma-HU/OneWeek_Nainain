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
    public float bgmValue;
    public float seValue;

    //public GameObject VolumeSettingUI;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        /*
        if(bgmVolumeText == null){
            bgmVolumeText = GameObject.Find("BGMVolumeText").GetComponent<TextMeshProUGUI>();
        }
        if(SEVolumeText == null){
            SEVolumeText = GameObject.Find("SEVolumeText").GetComponent<TextMeshProUGUI>();
        }
        */

        /*
        bgmValue = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>().value;
        seValue = GameObject.Find("SEVolumeSlider").GetComponent<Slider>().value;
        */

        // Load saved values from PlayerPrefs
        bgmValue = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        seValue = PlayerPrefs.GetFloat("SEVolume", 1.0f);


        // Apply the values to the sliders
        /*
        GameObject.Find("BGMVolumeSlider").GetComponent<Slider>().value = bgmValue;
        GameObject.Find("SEVolumeSlider").GetComponent<Slider>().value = seValue;
        */
    }
    
    void Start()
    {
        var sources = GetComponentsInChildren<AudioSource>();
        bgmSource = sources[0];
        Debug.Log(bgmSource);
        gameSoundContoroller = GetComponentInChildren<GameSoundController>();
        SceneManager.sceneLoaded += SceneLoaded;

        // Set the volume text
        bgmVolumeText.text = (bgmValue * 100).ToString("0.0");
        SEVolumeText.text = (seValue * 100).ToString("0.0");
    }

    void SceneLoaded (Scene nextScene, LoadSceneMode mode) {
        if(nextScene.name == "Title"){
            //ここらへんでサウンドのUIつくる
            /*
            Instantiate(VolumeSettingUI);
            */

            /*
            Slider[] sliders = FindObjectsOfType<Slider>(true);
            sliders[1].onValueChanged.AddListener(ChangeBGMVolume);
            sliders[0].onValueChanged.AddListener(ChangeGameSoundVolume);

            // Reapply the values to the sliders
            sliders[1].value = bgmValue;
            sliders[0].value = seValue;
            */
        }
    }

    public void ChangeBGMVolume(float value){
        bgmSource.volume = value;
        bgmVolumeText.text = (value * 100).ToString("0.0");
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }
    public void ChangeGameSoundVolume(float value){
        gameSoundContoroller.volume = value;
        SEVolumeText.text = (value * 100).ToString("0.0");
        PlayerPrefs.SetFloat("SEVolume", value);
        PlayerPrefs.Save();
    }
}
