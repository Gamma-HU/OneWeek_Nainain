using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    Vector2 prevPos; //保存しておく初期position
    Vector2 offSet;
    RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    ItemStock  itemStock;
    [SerializeField]string playerTagName;
    [SerializeField]int itemType;
    


    private void Start()
    {
      rectTransform = GetComponent<RectTransform>();
      parentRectTransform = rectTransform.parent as RectTransform;
      offSet = new Vector2(0, rectTransform.sizeDelta.y*0.5f);
      itemStock = GetComponentInParent<ItemStock>();
    }


    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        // RectTransformの場合はpositionではなくanchoredPositionを使う
        prevPos = rectTransform.anchoredPosition;

    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        // eventData.positionから、親に従うlocalPositionへの変換を行う
        // オブジェクトの位置をlocalPositionに変更する

        Vector2 localPosition = GetLocalPosition(eventData.position);
        rectTransform.anchoredPosition = localPosition + offSet;
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData) {
      Debug.Log($"ドラッグ終了");
      
      rectTransform.anchoredPosition = prevPos;
      Vector3 MousePoint = Input.mousePosition;
      MousePoint = Camera.main.ScreenToWorldPoint(MousePoint);

      //マウスのある位置から、奥(0, 0, 1)に向かってRayを発射（ワールド座標）
      RaycastHit2D[] hit2D = Physics2D.RaycastAll(MousePoint , Vector3.forward);
      foreach(RaycastHit2D hit in hit2D){

        if(hit.transform.gameObject.tag == playerTagName){
          Debug.Log("ItemUsed");
          itemStock.OnReduce(itemType);
        }
      }
    }

    // ScreenPositionからlocalPositionへの変換関数
  private Vector2 GetLocalPosition(Vector2 screenPosition)
  {
      Vector2 result = Vector2.zero;

      // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
      RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

      return result;
  }

}
  


