using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConbineMaltiplySwitch : MonoBehaviour
{
    [SerializeField]
    GameObject nainainUI, sanbainUI, conbineUIs;
    Image nainainImage, sanbainImage;
    

    void Start(){
        nainainImage = nainainUI.GetComponent<Image>();
        sanbainImage = sanbainUI.GetComponent<Image>();
    }
    bool isConbineMode = false;

    void MaltiplyMode(){
        nainainImage.enabled = true;
        sanbainImage.enabled = true;
        conbineUIs.SetActive(false);
    }

    void ConbineMode(){
        nainainImage.enabled = false;
        sanbainImage.enabled = false;
        conbineUIs.SetActive(true);
    }

    public void OnClick(){
        if(isConbineMode){
            MaltiplyMode();
        }else{
            ConbineMode();
        }
    }

}
