using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonController : MonoBehaviour
{
    public GameObject VolumePanel;
    void Start()
    {
        
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
