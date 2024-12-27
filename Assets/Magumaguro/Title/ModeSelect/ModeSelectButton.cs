using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectButton : MonoBehaviour
{
    public void OnNormalButtonClicked()
    {
        Debug.Log("Normal Button Clicked");
        //ノーマルモードに遷移する
        //SceneManager.LoadScene("");

    }

    public void OnEndlessButtonClicked()
    {
        Debug.Log("Endless Button Clicked");
        //エンドレスモードに遷移する
        //SceneManager.LoadScene("");

    }
}
