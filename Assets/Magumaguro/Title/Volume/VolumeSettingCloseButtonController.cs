using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeSettingCloseButtonController : MonoBehaviour
{
    private static GameObject StartButton;
    private static GameObject VolumeSettingButton;
    private static GameObject ResetButton;
    void Start()
    {
        StartButton = GameObject.Find("StartButton");
        VolumeSettingButton = GameObject.Find("VolumeSettingButton");
        ResetButton = GameObject.Find("ResetButton");
        Debug.Log("StartButton: " + StartButton);
        StartButton.SetActive(false);
        VolumeSettingButton.SetActive(false);
        ResetButton.SetActive(false);
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
        ResetButton = GameObject.Find("ResetButton");
        Debug.Log("StartButton: " + StartButton);
    }

    public void OnVolumeSettingCloseButtonClicked()
    {
        StartButton.SetActive(true);
        VolumeSettingButton.SetActive(true);
        ResetButton.SetActive(true);
    }
}
