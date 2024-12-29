using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]string itemTagName = "Item";
    [SerializeField]string dropTargetTagName = "Player";
    Item draggingItem;
    

    public void SetItem(Item draggingItem){
        this.draggingItem = draggingItem;
    }

    public void OnBeginDrag(PointerEventData eventData){
        PointerEventData pointData = new PointerEventData(EventSystem.current);
        pointData.position = Input.mousePosition;
        List<RaycastResult> RayResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointData, RayResult);
        foreach (RaycastResult result in RayResult){
            if(result.gameObject.name == itemTagName){
                var item = result.gameObject.GetComponent<Item>();
                SetItem(item);
                Debug.Log(result.gameObject.name);
            }
        }
    }

    public void OnDrag(PointerEventData eventData){
        // オブジェクトの位置をworldPositionに変更する
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if(draggingItem != null){
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            pointData.position = Input.mousePosition;
            List<RaycastResult> RayResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointData, RayResult);
            foreach (RaycastResult result in RayResult){
                if(result.gameObject.tag == dropTargetTagName){
                    Debug.Log(draggingItem);
                }
                draggingItem = null;
            }
        }
    }
}
