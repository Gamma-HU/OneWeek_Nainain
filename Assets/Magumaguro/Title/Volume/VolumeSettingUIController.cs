using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingUIController : MonoBehaviour
{
    /*
    private GameObject Audio;
    private AudioSetting AS;

    */
    
    public static VolumeSettingUIController instance;
    void Awake()
    {
        /*
        Audio = GameObject.Find("Audio");
        AS = Audio.GetComponent<AudioSetting>();
        */
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        
    void Start()
    {
        /*
        Slider[] sliders = FindObjectsOfType<Slider>(true);
        sliders[1].onValueChanged.AddListener(AS.ChangeBGMVolume);
        sliders[0].onValueChanged.AddListener(AS.ChangeGameSoundVolume);

        // Reapply the values to the sliders
        sliders[1].value = AS.bgmValue;
        sliders[0].value = AS.seValue;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
