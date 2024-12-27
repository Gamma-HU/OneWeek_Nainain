using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//IBeginDragHandler, IDragHandler, IEndDragHandlerを使うのに必要

public class ItemDragBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

  RectTransform rectTransform;
  ItemLine itemLine;
  void Start()
  {
    rectTransform = transform as RectTransform;
    itemLine = GetComponentInChildren<ItemLine>();
  }

  public void OnBeginDrag(PointerEventData eventData) {
    Debug.Log($"ドラッグ開始");
  }

  public void OnDrag(PointerEventData eventData) {
    itemLine.Draw(eventData.position);
  }

  public void OnEndDrag(PointerEventData eventData) {
    //image.raycastTarget = false;
    //Debug.Log(eventData.pointerEnter.name);
    Debug.Log($"ドラッグ終了");
    //image.raycastTarget = true;
  }
}
