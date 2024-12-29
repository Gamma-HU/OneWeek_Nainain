using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GouseiBaibaiSwitch : MonoBehaviour
{
    [SerializeField]
    GameObject nainainUI, sanbainUI, kurimanjuUIs;

    bool isConbineMode = false;

    void baibaiMode(){
        nainainUI.SetActive(true);
        sanbainUI.SetActive(true);
        kurimanjuUIs.SetActive(false);
    }

    void ConbineMode(){
        nainainUI.SetActive(false);
        sanbainUI.SetActive(false);
        kurimanjuUIs.SetActive(true);
    }

    public void OnClick(){
        if(isConbineMode){
            baibaiMode();
        }else{
            ConbineMode();
        }
    }

}
