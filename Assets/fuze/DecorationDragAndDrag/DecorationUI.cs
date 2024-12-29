using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationUI : MonoBehaviour // これをドラッグするためのUI要素のプレハブ（DecoPanel）につけて、生成するときに適当なDecorationをセットしてほしい。
{
    
    Decoration decoration;
    public Decoration GetDeco(){
        return decoration;
    }
    public void SetDeco(Decoration decoration){
        this.decoration = decoration;
    }

}
