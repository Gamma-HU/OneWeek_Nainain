using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonController : MonoBehaviour
{
    private static Transform parent;
    private static Transform child;
    private GameObject VolumePanel;
    void Start()
    {
        parent = GameObject.Find("VolumeSettingCanvas").transform;
        child = parent.Find("VolumeSettingUI");
        VolumePanel = child.gameObject;
    }

    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked");
    }

    public void OnVolumeSettingButtonClicked()
    {
        VolumePanel.SetActive(true);
    }
}
