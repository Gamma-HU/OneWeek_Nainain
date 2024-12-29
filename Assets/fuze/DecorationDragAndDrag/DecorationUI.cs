using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationUI : MonoBehaviour // これをドラッグするためのUI要素のプレハブ（DecorationPanel）につけて、生成するときに適当なDecorationとSpriteをセットしてほしい。
{
    
    Decoration decoration;
    Sprite sprite;


    public Decoration GetDeco(){
        return decoration;
    }
    public void SetDeco(Decoration decoration){
        this.decoration = decoration;
    }

    public Sprite GetSprite(){
        return sprite;
    }
    public void SetSprite(Sprite sprite){
        this.sprite = sprite;
    }

}
