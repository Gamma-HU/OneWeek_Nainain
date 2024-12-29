using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]string itemTagName = "Item";
    [SerializeField]string dropTargetTagName = "Player";
    [SerializeField]string conbineItemName = "KurimanjuUI";
    [SerializeField]string tripleItemName = "Sanbain";
    [SerializeField]string nonupleItemName = "Nainain";

    [SerializeField]
    GameObject draggingUIObj;
    DraggingUI draggingUI;

    Item draggingItem;

    [System.Serializable]
    public class GouseiCallback : UnityEngine.Events.UnityEvent<Kurimanju>{}
    GouseiCallback ConbineiEvent;
    UnityEngine.Events.UnityEvent  TripleEvent;
    UnityEngine.Events.UnityEvent  NonupleEvent;

    void Start(){
        draggingUI = draggingUIObj.GetComponent<DraggingUI>();
    }

    public void SetDraggingUI(Item draggingItem){
        this.draggingItem = draggingItem;
        draggingUI.SetSprite(draggingItem.Image().sprite);
    }

    public void OnBeginDrag(PointerEventData eventData){
        PointerEventData pointData = new PointerEventData(EventSystem.current);
        pointData.position = Input.mousePosition;
        List<RaycastResult> RayResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointData, RayResult);
        foreach (RaycastResult result in RayResult){
            if(result.gameObject.name == itemTagName){
                var item = result.gameObject.GetComponent<Item>();
                SetDraggingUI(item);
                Debug.Log(result.gameObject.name);
            }
        }
    }

    public void OnDrag(PointerEventData eventData){
    }

    public void OnEndDrag(PointerEventData eventData) {
        if(draggingItem != null){
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            pointData.position = Input.mousePosition;
            List<RaycastResult> RayResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointData, RayResult);
            foreach (RaycastResult result in RayResult){
                if(result.gameObject.tag == dropTargetTagName){
                    if(result.gameObject.name == conbineItemName){
                        Debug.Log("gousei");
                        Debug.Log(draggingItem.Hontai());
                    }else if(result.gameObject.name == tripleItemName){
                        Debug.Log("sanbai");
                    }else if(result.gameObject.name == nonupleItemName){
                        Debug.Log("tyuubai");
                    }
                    Debug.Log(draggingItem);
                    draggingItem = null;
                }
            }
        }
    }
}
