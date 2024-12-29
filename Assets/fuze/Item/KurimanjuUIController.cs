using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KurimanjuUIController : MonoBehaviour
{
    [SerializeField]
    GameObject kurimanjuUIPrefab;
    [SerializeField]
    GameObject canvas;

    RectTransform parentRectTransform;

    GameObject kurimanju;
    GameObject kurimanjuUI;

    void Start(){
        kurimanju = gameObject;
        parentRectTransform = canvas.transform as RectTransform;
        Debug.Log("CreateKurimanjuUI!!!");
        CreateKurimanjuUI();
        SetKurimanjuUIPosition();
    }

    void CreateKurimanjuUI(){
        
        kurimanjuUI = Instantiate(kurimanjuUIPrefab, Vector3.zero, Quaternion.identity, canvas.transform as RectTransform);
        kurimanjuUI.GetComponent<Item>().SetHontai(GetComponent<Kurimanju>());
    }

    void DestroyKurimanjuUI(){
        Destroy(kurimanjuUI);
    }

    void SetImageEnableKurimanjuUI(bool enabled){
        kurimanjuUI.GetComponent<Image>().enabled = enabled;
    }

    void SetKurimanjuUIPosition(){
        
        RectTransform kurimanjuUIRectTransform = kurimanjuUI.transform as RectTransform;

        Vector2 result = Vector2.zero;
        var screenPosition = Camera.main.WorldToScreenPoint(kurimanju.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);
        
        kurimanjuUIRectTransform.anchoredPosition3D  = new Vector3 (result.x, result.y, 0);
    }
}
