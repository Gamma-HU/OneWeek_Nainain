using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStock : MonoBehaviour
{
    int nainainStock = 0;
    int sanbainStock = 0;
    int gouseiStock = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReduce(int itemType){
        if(itemType == 0){
            nainainStock--;
        }else if(itemType == 1){
            sanbainStock--;
        }else{
            gouseiStock--;
        }
        Debug.Log("REduced"+itemType);
    }
    public void OnIncreace(int itemType){
        if(itemType == 0){
            nainainStock++;
        }else if(itemType == 1){
            sanbainStock++;
        }else{
            gouseiStock++;
        }
        Debug.Log("Increaced"+itemType);
    }
}
