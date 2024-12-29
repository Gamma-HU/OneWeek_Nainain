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
    GouseiCallback ConbineEvent;
    UnityEngine.Events.UnityEvent  TripleEvent;
    UnityEngine.Events.UnityEvent  NonupleEvent;

    void Start(){
        
    }

    public void SetDraggingUI(Item draggingItem){
        this.draggingItem = draggingItem;
        draggingUI.GetComponent<Image>().enabled = true;
        draggingUI.GetComponent<Image>().sprite = draggingItem.Image().sprite;
        Debug.Log("SetDraggingUI!!");
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
                Debug.Log(result.gameObject.name + "is target? ==" + (result.gameObject.name == conbineItemName || result.gameObject.name == tripleItemName || result.gameObject.name == nonupleItemName));
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
                        if(draggingItem.name == conbineItemName){
                            Debug.Log("gousei");
                            Debug.Log(draggingItem.Hontai());
                        }else if(draggingItem.name == tripleItemName){
                            Debug.Log("sanbai");
                        }else if(draggingItem.name == nonupleItemName){
                            Debug.Log("kyuubai");
                        }
                        Debug.Log(draggingItem);
                        draggingItem = null;
                    }
                }
            }
            draggingItem = null;
            draggingUI.GetComponent<Image>().enabled = false;
        }
    }
}
