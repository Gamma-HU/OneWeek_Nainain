using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    Vector2 prevPos; //保存しておく初期position

    RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    ItemStock  itemStock;
    [SerializeField]string playerTagName;
    [SerializeField]int itemType;
    
    private void Start()
    {
      itemStock = GetComponentInParent<ItemStock>();
    }

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
      Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      MousePoint.z = 0;

      //マウスのある位置から、奥(0, 0, 1)に向かってRayを発射（ワールド座標）
      RaycastHit2D[] hit2D = Physics2D.RaycastAll(MousePoint , Vector3.forward);
      foreach(RaycastHit2D hit in hit2D){

        if(hit.transform.gameObject.tag == playerTagName){
          Debug.Log("ItemUsed");
        }
      }
    }

}
