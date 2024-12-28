using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManjuUIState : MonoBehaviour
{
    Vector2 offSet;  // アンカーの位置でずれるのを防ぐ
    RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    RectTransform parentRectTransform; // 移動したいオブジェクトの親(Canvas)のRectTransform
    [SerializeField]public GameObject KurimanjuHonati;  //これを登録しなければならない
    Image image;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
        image = GetComponent<Image>();
        offSet = new Vector2(0,0);
        //InVisible();
        InitializePosition();
    }

    void InitializePosition(){
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.parent.gameObject.transform.position);
        Vector2 localPosition = GetLocalPosition(screenPosition);
        rectTransform.anchoredPosition = localPosition + offSet;
    }
    
    void Visble(){
        image.enabled = true;
    }

    void InVisible(){
        image.enabled = false;
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }
}
