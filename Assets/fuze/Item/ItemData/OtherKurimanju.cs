using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherKurimanju : Item
{
    [System.Serializable]
    public class GouseiCallback : UnityEngine.Events.UnityEvent<Kurimanju>{}
    [SerializeField]
    GouseiCallback onDropEventWithArgument;

    Kurimanju hontai;

    void SetHontai(Kurimanju hontai){
        this.hontai = hontai;
    }

    public override void OnDrop(){
        Debug.Log("OnDropFunctionWithArgument");
        onDropEventWithArgument.Invoke(hontai);
    }
}
