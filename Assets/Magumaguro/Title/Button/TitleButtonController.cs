using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonController : MonoBehaviour
{
    private static Transform parent;
    private static Transform child;
    private GameObject VolumePanel;

    public GameObject EndlessButton;
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

    public void OnResetButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        EndlessButton.GetComponent<Button>().interactable = false;
    }
}
