using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DecorationDragAndDropManager : MonoBehaviour
{
    [SerializeField]string dropTargetTagName = "Player";  // ドラッグされているUIが落とされるKurimanjuUI(Clone)に付けられたタグ。

    [SerializeField]string DecorationUIName = "Nainain";  // ドラッグ可能な装飾品のUIの名前。

    [SerializeField]
    GameObject draggingUI; // ドラッグ中のUI要素を表示するGameObject
    Image draggingUIImage;
    
    Decoration draggingDecoration;  // 掴んでいる、使う予定の装飾品の情報。

    DropPoint dropPoint; //KurimanjuUIについていて、ドロップしたUIの本体のKurimanjuスクリプトを知っている。

    void Start(){
        draggingUIImage = draggingUI.GetComponent<Image>();
    }


    void DecorationDropedActin(Kurimanju target){  // targetが、アイテムが使われるKurimanjuスクリプト。合成先。
        Debug.Log("ターゲット:" + target + "デコ:" + draggingDecoration);
        //target.Equip(GameObject deco, int rank) Equipを呼ぶ。
    }

    public void OnClickItem(GameObject clickDeco){
        this.draggingDecoration = clickDeco.GetComponent<Decoration>();
        draggingUIImage.enabled = true;
        draggingUIImage.sprite = draggingDecoration.sprite;
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
                if(getObj.name == DecorationUIName){

                    OnClickItem(getObj);
                }
                //Debug.Log(result.gameObject.name + "is target? ==" + (result.gameObject.tag == dropTargetTagName));
            }
        }
        if(Input.GetMouseButtonUp(0)){
            if(draggingDecoration != null){
                PointerEventData pointData = new PointerEventData(EventSystem.current);
                pointData.position = Input.mousePosition;
                List<RaycastResult> RayResult = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointData, RayResult);
                foreach (RaycastResult result in RayResult){
                    if(result.gameObject.tag == dropTargetTagName){

                        Kurimanju target = result.gameObject.GetComponent<DropPoint>().Hontai(); // これこそが装飾品を装備するKurimanjuのスクリプト

                        DecorationDropedActin(target);
                    }
                }
            }
            draggingDecoration = null;
            draggingUIImage.enabled = false;
        }
    }
}
