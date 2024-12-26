using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    [SerializeField]
    public float bgmVolume, gameSoundVolume; //配下のBGMCOntroller  やGameSOundCOntrollerから読まれます。
    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        
    }

    void ChangeBGMVolume(float value){
        bgmVolume = value;
    }
    void ChangeGameSoundVolume(float value){
        gameSoundVolume = value;
    }
}
