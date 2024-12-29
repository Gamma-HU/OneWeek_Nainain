using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    Image image;

    Kurimanju hontai; // OtherKurimanjuの場合だけ設定される

    
    void Start()
    {
        image = GetComponent<Image>();
    }
    public Image Image(){
        return image;
    }
    public Kurimanju Hontai(){
        return hontai;
    }
    public void SetHontai(Kurimanju hontai){
        this.hontai = hontai;
    }
}

