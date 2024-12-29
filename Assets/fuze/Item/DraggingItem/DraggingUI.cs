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
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = worldPosition;
        }
    }
}
