using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField]string dropTargetTagName = "Player";
    GameObject draggingItem;
    
    void Start()
    {
        draggingItem = gameObject;
    }

    void SetItem(){

    }

    void Update()
    {
        if(draggingItem != null){
            if(Input.GetMouseButtonUp(0)){
                PointerEventData pointData = new PointerEventData(EventSystem.current);
                pointData.position = Input.mousePosition;
                List<RaycastResult> RayResult = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointData, RayResult);
                foreach (RaycastResult result in RayResult)
                {
                    if(result.gameObject.tag == dropTargetTagName){
                        Debug.Log(draggingItem);
                    }
                    draggingItem = null;
                }
            }
        }
    }
}
