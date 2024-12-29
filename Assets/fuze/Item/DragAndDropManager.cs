using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField]string dropTargetTagName = "Player";
    [SerializeField]string conbineItemName = "KurimanjuUI";
    [SerializeField]string tripleItemName = "Sanbain";
    [SerializeField]string nonupleItemName = "Nainain";

    [SerializeField]
    GameObject draggingUI;

    Item draggingItem;

    [System.Serializable]
    public class GouseiCallback : UnityEngine.Events.UnityEvent<Kurimanju>{ }
    GouseiCallback ConbineiEvent;
    UnityEngine.Events.UnityEvent  TripleEvent;
    UnityEngine.Events.UnityEvent  NonupleEvent;

    void Start(){
        
    }

    public void SetDraggingUI(Item draggingItem){
        draggingUI.GetComponent<Image>().enabled = true;
        this.draggingItem = draggingItem;
        draggingUI.GetComponent<Image>().sprite = draggingItem.Image().sprite;
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            pointData.position = Input.mousePosition;
            List<RaycastResult> RayResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointData, RayResult);
            foreach (RaycastResult result in RayResult){
                //Debug.Log(result.gameObject.name);
                if(result.gameObject.name == conbineItemName || result.gameObject.name == tripleItemName || result.gameObject.name == nonupleItemName){

                    var item = result.gameObject.GetComponent<Item>();
                    SetDraggingUI(item);
                }
            }
        }
        if(Input.GetMouseButtonUp(0)){
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
            draggingUI.GetComponent<Image>().enabled = false;
        }
    }
}
