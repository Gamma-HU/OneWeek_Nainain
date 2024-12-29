using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropPoint : MonoBehaviour  //　アイテムや装飾品がドロップされたときに対応する栗饅頭を呼び出す。
// 栗饅頭の合成の際には合成元の栗饅頭の情報としても使われる。
{
    Kurimanju hontai; // OtherKurimanjuの場合だけ設定される
    
    public Kurimanju Hontai(){
        return hontai;
    }
    public void SetHontai(Kurimanju hontai){  // SetHontaiはKurimanjuUIControllerから呼ばれる。
        this.hontai = hontai;
    }
}

