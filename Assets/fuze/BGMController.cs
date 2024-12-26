using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField]
    AudioClip titleBGM,mainGameBGM;
    [SerializeField]
    AudioSource source;
    void Start()
    {
        TitleBGM();
    }

    public void TitleBGM(){
        source.clip = titleBGM;
        source.Play();
    }
    public void MainGameBGM(){
        source.clip = mainGameBGM;
        source.Play();
    }
    public void Stop(){
        source.Stop();
    }
}
