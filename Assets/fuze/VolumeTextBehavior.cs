using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeTextBehavior : MonoBehaviour
{
    TextMeshProUGUI tmp;
    [SerializeField]
    float initialVolume = 0.5f;
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = ChangeText(initialVolume);
    }

    public void OnValueChange(float value){
        tmp.text = ChangeText(value);
    }
    string ChangeText(float number){
        int intValue = (int)(number*100);
        string strValue = intValue.ToString();
        return strValue;
    }
}
