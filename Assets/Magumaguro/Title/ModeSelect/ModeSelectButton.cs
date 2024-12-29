using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectButton : MonoBehaviour
{
    private GameSoundController GSController;

    void Start()
    {
        GSController = GameObject.Find("Game Sound").GetComponent<GameSoundController>();
    }
    public void OnNormalButtonClicked()
    {
        Debug.Log("Normal Button Clicked");
        //ノーマルモードに遷移する
        //一旦仮のシーンへ遷移。後で変更してください
        SceneManager.LoadScene("Kari");
        //GSController.Maltiply();
    }

    public void OnEndlessButtonClicked()
    {
        Debug.Log("Endless Button Clicked");
        //エンドレスモードに遷移するようにしてください
        //SceneManager.LoadScene("");

        //GSController.Maltiply();

    }
}
