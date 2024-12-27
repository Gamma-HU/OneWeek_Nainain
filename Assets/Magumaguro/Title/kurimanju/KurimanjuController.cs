using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurimanjuController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float rotationSpeed;
    private float xSpeed;

    [SerializeField] private ParticleSystem kurimanjuParticle;
    void Start()
    {
        rotationSpeed = Random.Range(-360.0f, 360.0f);
        xSpeed = Random.Range(-0.05f, 0.05f);
        StartCoroutine(liftTime());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(xSpeed * Time.deltaTime, -speed * Time.deltaTime, 0), Space.World);
        this.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private IEnumerator liftTime()
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(this.gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy (this.gameObject);
    }

    public void OnClicked()
    {
        kurimanjuParticle.Play();
        Debug.Log("Kurimanju Clicked");
    }
}
