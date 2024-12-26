using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Motion());
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Motion()
    {
        for (int i = 0; i < 45; i++)
        {
            transform.position = transform.position + new Vector3(0, -0.1f, 0);
            yield return null;
        }
        
    }
}
