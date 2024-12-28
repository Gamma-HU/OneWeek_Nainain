using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemEffectTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnNainainDropEvent(){
        Debug.Log("Nainain!!");
    }
    public void OnSanbainDropEvent(){
        Debug.Log("Sanbain!!");
    }
    public void OnOtherKurimanjuUIDropEvent(){
        Debug.Log("Gousei!!");
    }
    public void OnGouseiDropEvent(GameObject gouseimoto){
        Debug.Log(gouseimoto+"Gousei!!");
    }
    
}