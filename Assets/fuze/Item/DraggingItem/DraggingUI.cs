using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggingUI : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public Image Image(){
        return image;
    }

    public void SetSprite(Sprite sprite){
        image.sprite = sprite;
    }
    void Update(){
        if(Input.GetMouseButton(0)){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            transform.position = worldPosition;
        }
    }
}
