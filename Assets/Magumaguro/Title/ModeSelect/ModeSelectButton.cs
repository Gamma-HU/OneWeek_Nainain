using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectButton : MonoBehaviour
{
    public void OnNormalButtonClicked()
    {
        Debug.Log("Normal Button Clicked");
        //ノーマルモードに遷移する
        //一旦仮のシーンへ遷移。後で変更する
        SceneManager.LoadScene("Kari");

    }

    public void OnEndlessButtonClicked()
    {
        Debug.Log("Endless Button Clicked");
        //エンドレスモードに遷移する
        //SceneManager.LoadScene("");

    }
}
