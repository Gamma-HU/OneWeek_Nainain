using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 必要

public class TankDropHandlerExample : MonoBehaviour, IDropHandler  // 継承する
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped object was: " + droppedObject);
        }
    }

}
