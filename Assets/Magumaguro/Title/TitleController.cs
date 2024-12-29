using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject EndlessButton;
    private int EndlessFlag;
    void Start()
    {
        //エンドレス解放されたか確認
        EndlessFlag = PlayerPrefs.GetInt("EndlessFlag", 0);
        if(EndlessFlag == 1) EndlessButton.GetComponent<Button>().interactable = true;
        else EndlessButton.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
