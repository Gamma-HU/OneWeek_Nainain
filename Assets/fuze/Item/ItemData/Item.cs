using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Events.UnityEvent onDropEvent;

    Image image;

    
    void Start()
    {
        image = GetComponent<Image>();
    }
    public virtual void OnDrop(){
        Debug.Log("OnDropFunctionWithNoArgument");
        onDropEvent.Invoke();
    }
}

