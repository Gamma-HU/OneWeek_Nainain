using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeSettingCloseButtonController : MonoBehaviour
{
    private static GameObject StartButton;
    private static GameObject VolumeSettingButton;
    void Start()
    {
        StartButton = GameObject.Find("StartButton");
        VolumeSettingButton = GameObject.Find("VolumeSettingButton");
        Debug.Log("StartButton: " + StartButton);
        StartButton.SetActive(false);
        VolumeSettingButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*

    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if(nextScene.name == "Title")
        {
            StartButton = GameObject.Find("StartButton");
            VolumeSettingButton = GameObject.Find("VolumeSettingButton");
            Debug.Log("StartButton: " + StartButton);
        }
    }
    */

    private void OnEnable()
    {
        StartButton = GameObject.Find("StartButton");
        VolumeSettingButton = GameObject.Find("VolumeSettingButton");
        Debug.Log("StartButton: " + StartButton);
    }

    public void OnVolumeSettingCloseButtonClicked()
    {
        StartButton.SetActive(true);
        VolumeSettingButton.SetActive(true);
    }
}
