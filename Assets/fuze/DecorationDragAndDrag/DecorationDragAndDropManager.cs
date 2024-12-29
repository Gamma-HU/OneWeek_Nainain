using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DecorationDragAndDropManager : MonoBehaviour
{
    [SerializeField]string dropTargetTagName = "Player";  // ドラッグされているUIが落とされるKurimanjuUI(Clone)に付けられたタグ。

    [SerializeField]string DecorationUIName = "DecorationPanel(Clone)";  // ドラッグ可能な装飾品のUIの名前。

    [SerializeField]
    GameObject draggingUI; // ドラッグ中のUI要素を表示するGameObject
    Image draggingUIImage;
    
    DecorationUI draggingDecoration;  // 掴んでいる、使う予定の装飾品の情報。

    DropPoint dropPoint; //KurimanjuUIについていて、ドロップしたUIの本体のKurimanjuスクリプトを知っている。

    void Start(){
        draggingUIImage = draggingUI.GetComponent<Image>();
    }


    void DecorationDropedActin(Kurimanju target){  // targetが、アイテムが使われるKurimanjuスクリプト。合成先。
        Debug.Log("ターゲットのレベル:" + target.Status().level + "デコ:" + draggingDecoration.GetDeco().Status().decoName);
        //target.Equip(GameObject deco, int rank) Equipを呼ぶ。
    }

    public void OnClickItem(GameObject clickDeco){
        this.draggingDecoration = clickDeco.GetComponent<DecorationUI>();  // draggingDecorationに情報を登録
        draggingUIImage.enabled = true;
        draggingUIImage.sprite = draggingDecoration.GetSprite();  // draggingUIImageに画像を表示
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
                List<RaycastResult> rayResult = RaycastAll();
                foreach (RaycastResult result in rayResult){
                var getObj = result.gameObject;
                if(getObj.name == DecorationUIName){

                    OnClickItem(getObj);
                }
                //Debug.Log(result.gameObject.name + "is target? ==" + (result.gameObject.tag == dropTargetTagName));
            }
        }
        if(Input.GetMouseButtonUp(0)){
            if(draggingDecoration != null){
                List<RaycastResult> rayResult = RaycastAll();
                foreach (RaycastResult result in rayResult){
                    if(result.gameObject.tag == dropTargetTagName){

                        Kurimanju target = result.gameObject.GetComponent<DropPoint>().Hontai(); // これこそが装飾品を装備するKurimanjuのスクリプト

                        DecorationDropedActin(target);
                    }
                }
            }
            draggingDecoration = null;
            draggingUIImage.enabled = false;
        }
        List<RaycastResult> RaycastAll(){
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            pointData.position = Input.mousePosition;
            List<RaycastResult> rayResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointData, rayResult);
            return rayResult;
        }
    }
}
