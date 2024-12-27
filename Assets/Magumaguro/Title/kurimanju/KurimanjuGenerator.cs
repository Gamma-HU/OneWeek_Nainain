using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurimanjuGenerator : MonoBehaviour
{
    public GameObject kurimanjuPrefab;

    void Start()
    {
        StartCoroutine(GenerateKurimanju());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator GenerateKurimanju()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);

            float x = Random.Range(-6.0f, 6.0f);
            float rotation = Random.Range(0.0f, 360.0f);
            Instantiate(kurimanjuPrefab, new Vector3(x, 6.0f, 0.0f), Quaternion.Euler(0, 0, rotation));
        }
    }
}
