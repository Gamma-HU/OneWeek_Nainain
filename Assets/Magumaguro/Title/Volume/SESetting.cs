using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SESetting : MonoBehaviour,IPointerUpHandler
{
    AudioSource audioSource;
    private GameObject Audio;
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("SE");
        //audioSource.PlayOneShot(audioSource.clip);
        Audio.GetComponent<GameSoundController>().Shoot2();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Audio = GameObject.Find("Game Sound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
