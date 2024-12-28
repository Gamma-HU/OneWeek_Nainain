using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
  [SerializeField]
  private UnityEngine.Events.UnityEvent OnItemUsed;  // これでアイテムを減らす処理をする。
  [SerializeField]
  string playerTagName = "Player";  // 自機を判別するためのタグ
  [SerializeField]
  public GameObject hontai;  // ※栗饅頭の合成用のUIだけが必要とする。

  Vector2 prevPos; //保存しておく初期position
  RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform
  ItemStock  itemStock;
  

  // ドラッグ開始時の処理
  public void OnBeginDrag(PointerEventData eventData)
  {
    // ドラッグ前の位置を記憶しておく
    prevPos = transform.position;
  }

  // ドラッグ中の処理
  public void OnDrag(PointerEventData eventData)
  {
    // オブジェクトの位置をworldPositionに変更する
    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
    transform.position = worldPosition;
  }

  // ドラッグ終了時の処理
  public void OnEndDrag(PointerEventData eventData) {
    
    transform.position = prevPos;
    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePoint.z = 0;

    List<RaycastResult> RayResult= new List<RaycastResult>(); 
    EventSystem.current.RaycastAll(eventData , RayResult);
    foreach (RaycastResult result in RayResult)
    {
      //Debug.Log("raycastresult"+result.gameObject.tag);
      if(result.gameObject.tag == playerTagName){
        Debug.Log("Used!!");
        OnItemUsed.Invoke();
      }
    }
  }

}
