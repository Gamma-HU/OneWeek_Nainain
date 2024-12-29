using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField]string dropTargetTagName = "Player";  // ドラッグされているUIが落とされるKurimanjuUI(Clone)に付けられたタグ。

    [SerializeField]string conbineItemName = "KurimanjuUI(Clone)";
    [SerializeField]string tripleItemName = "Sanbain";
    [SerializeField]string nonupleItemName = "Nainain";  // それぞれはUIのGameObjectの名前。

    [SerializeField]
    GameObject draggingUI; // ドラッグ中のUI要素を表示するGameObject

    Image draggingUIImage;
    string draggingItemName;  // 掴んでいる、使う予定のアイテムの情報。
    DropPoint draggingItem; //ドロップしたときにGameObjectを呼ぶ

    void Start(){
        draggingUIImage = draggingUI.GetComponent<Image>();
    }


    void ItemDropedActin(Kurimanju target){  // targetが、アイテムが使われるKurimanjuスクリプト。合成先。
        if(draggingItem.name == conbineItemName){

            Debug.Log("gousei");

            Debug.Log("合成元は" +  draggingItem.Hontai()); // Hontai()ではKurimanjuが取得できる 
        }else if(draggingItem.name == tripleItemName){

            Debug.Log("sanbai");

        }else if(draggingItem.name == nonupleItemName){

            Debug.Log("kyuubai");

        }
    }

    public void OnClickItem(GameObject clickItem){
        this.draggingItemName = clickItem.name;
        draggingUIImage.enabled = true;
        draggingUIImage.sprite = clickItem.GetComponent<Image>().sprite;
        Debug.Log("SetDraggingUI!!");
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            pointData.position = Input.mousePosition;
            List<RaycastResult> RayResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointData, RayResult);
            foreach (RaycastResult result in RayResult){
                var getObj = result.gameObject;
                if(getObj.name == conbineItemName || getObj.name == tripleItemName || getObj.name == nonupleItemName){

                    OnClickItem(getObj);
                }
                //Debug.Log(result.gameObject.name + "is target? ==" + (result.gameObject.tag == dropTargetTagName));
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

                        Kurimanju target = result.gameObject.GetComponent<DropPoint>().Hontai(); // これこそがアイテムが使われたり合成されたりするKurimanjuスクリプト

                        ItemDropedActin(target);
                        draggingItem = null;
                    }
                }
            }
            draggingItem = null;
            draggingUIImage.enabled = false;
        }
    }
}
