using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KurimanjuUIController : MonoBehaviour // それぞれの栗饅頭についていると想定。
{
    [SerializeField]
    GameObject kurmanjuUIs;  // Fuze/Item/ItemUICanvasの子としてある。　生成されるUIの親になる

    [SerializeField]
    GameObject kurimanjuUIPrefab;  // 生成すべきKurimanjuUIのプレハブ。 Fuze/Item/ItemData/kurimanjuUIにある。
    Kurimanju kurimanju;  // この栗饅頭についているKurimanjuスクリプト
    GameObject kurimanjuUI; // 生成されたKurimanjuUIを格納

    void Start(){

        kurimanju = GetComponent<Kurimanju>(); // この栗饅頭についているKurimanju
        Debug.Log("CreateKurimanjuUI!!!");
        CreateKurimanjuUI();
        SetKurimanjuUIPosition();
    }

    void CreateKurimanjuUI(){
        
        kurimanjuUI = Instantiate(kurimanjuUIPrefab, Vector3.zero, Quaternion.identity, kurmanjuUIs.transform);  // ItemUICanvasの子のkurmanjuUIsの子として生成される
        kurimanjuUI.GetComponent<DropPoint>().SetHontai(kurimanju); // KurimanjuUIにこのオブジェクトのKurimanjuスクリプトを渡す
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(kurmanjuUIs.transform as RectTransform, screenPosition, Camera.main, out result);
        
        kurimanjuUIRectTransform.anchoredPosition3D  = new Vector3 (result.x, result.y, 0);
    }
}
