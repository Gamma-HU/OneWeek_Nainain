using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurimanjuItemTest : MonoBehaviour
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
    public void OnGouseiDropEvent(GameObject gouseimoto){
        Debug.Log(gouseimoto+"Gousei!!");
    }
}
