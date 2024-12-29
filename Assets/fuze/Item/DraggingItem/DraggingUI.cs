using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggingUI : MonoBehaviour, IDragHandler
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public Image Image(){
        return image;
    }

    public void SetSprite(Sprite sprite){
        image.sprite = sprite;
    }

    public void OnDrag(PointerEventData eventData){
        // オブジェクトの位置をworldPositionに変更する
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = worldPosition;
    }
}
