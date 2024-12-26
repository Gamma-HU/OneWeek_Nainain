using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNormalButtonClicked()
    {
        Debug.Log("Normal Button Clicked");
        //ノーマルモードに遷移する

    }

    public void OnEndlessButtonClicked()
    {
        Debug.Log("Endless Button Clicked");
        //エンドレスモードに遷移する

    }
}
