using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 必要

public class TankDropHandler: MonoBehaviour, IDropHandler  // 継承する
{
    [System.Serializable]
    public class GouseiCallback : UnityEngine.Events.UnityEvent<GameObject>
    {

    }  // 引数付きのイベントを使うため

    [SerializeField]
    private UnityEngine.Events.UnityEvent OnNainainDropEvent;  // ナイナインを使った時に実行する関数
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnSanbainDropEvent;  // サンバインを使った時の処理
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnOtherKurimanjuDropEvent;  // 合成時の処理（引数なし）
    [SerializeField]
    private GouseiCallback GouseiEvent;  // 合成する時の処理(合成先の栗饅頭用、GameObjectが引数)

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            if(droppedObject.name == "Nainain"){
                OnNainainDropEvent.Invoke();
            }else if(droppedObject.name == "Sanbain"){
                OnSanbainDropEvent.Invoke();
            }else if(droppedObject.name == "KurimanjuGouseiUI"){
                OnOtherKurimanjuDropEvent.Invoke();
                GouseiEvent.Invoke(droppedObject.GetComponent<ManjuUIState>().KurimanjuHonati);
            }
        }
    }

}
