using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void SetInfoString(string setText)
    {
        GameObject text = transform.GetChild(1).gameObject;
        text.GetComponent<TextMeshProUGUI>().text = setText;
    }
}
