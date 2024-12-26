using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReaction : MonoBehaviour
{
    void Start()
    {
        /*
        // 事前にButtonにEventTriggerコンポーネントを追加しておく
        var trigger = this.GetComponent<EventTrigger>();
        // 登録するイベントを設定する
        var entry = new EventTrigger.Entry();
        //マウスオーバーしたら…
        entry.eventID = EventTriggerType.PointerEnter;

        // リスナーは単純にLogを出力するだけの処理にする
        entry.callback.AddListener((data) => { Debug.Log("PointerEnter"); });

        // イベントを登録する
        trigger.triggers.Add(entry);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PointerEnter()
    {
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void PointerExit()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void PointerDown()
    {
        this.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    public void PointerUp()
    {
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
}
