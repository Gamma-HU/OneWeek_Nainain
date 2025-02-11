using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class DecorationManager : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    public List<GameObject> decorations = new(); // 渡されたゲームオブジェクトを格納するリスト
    public List<GameObject> panels = new(); // 渡されたゲームオブジェクトをもとに生成したボタンを格納するリスト
    [SerializeField] GameObject viewContent;
    [SerializeField] GameObject testPrefab;
    [SerializeField] GameObject decolationPanel; // ボタンのプレハブ
    public static DecorationManager instance;
    public List<GameObject> AddDecolation(GameObject decoration)
    {
        decorations.Add(decoration);
        GameObject panel = Instantiate(decolationPanel, viewContent.transform);
        panel.GetComponent<Image>().sprite = decoration.GetComponent<SpriteRenderer>().sprite; // ボタンの画像をゲームオブジェクトをもとに設定
        panel.GetComponent<DecorationUI>().SetDeco(decoration.GetComponent<Decoration>()); // ボタンのDecorationUIにゲームオブジェクトのDecorationを設定
        panel.GetComponent<DecorationUI>().SetSprite(decoration.GetComponent<SpriteRenderer>().sprite); // ボタンのDecorationUIにゲームオブジェクトのSpriteを設定
        panels.Add(panel);
        return decorations;
    }
    public List<GameObject> RemoveDecolation(GameObject decoration)
    {
        Destroy(panels[decorations.IndexOf(decoration)]);
        panels.Remove(panels[decorations.IndexOf(decoration)]);
        decorations.Remove(decoration);
        return decorations;
    }
    //[ContextMenu("AddTest")]
    //public void Test()
    //{
    //    AddDecolation(Instantiate(testPrefab));
    //}
    //[ContextMenu("RemoveTest")]
    //public void Test2()
    //{
    //    RemoveDecolation(decorations[0]);
    //}
}
