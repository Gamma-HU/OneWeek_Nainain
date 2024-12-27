using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLine : MonoBehaviour
{
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = GetComponent<RectTransform>().position;
    }

    public void Draw(Vector3 endPos){
        Debug.Log(this.startPos+"to"+endPos);
    }
}
