using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundController : MonoBehaviour
{
    [SerializeField]
    AudioClip shoot1, shoot2, damage, equip, maltiply;
    [SerializeField]
    AudioSource source;
    
    public float volume = 1.0f;

    void Start()
    {
        
    }

    public void Shoot1(){
        source.PlayOneShot(shoot1, volume);
    }
    public void Shoot2(){
        source.PlayOneShot(shoot2, volume);
    }
    public void Damage(){
        source.PlayOneShot(damage, volume);
    }
    public void Equip(){
        source.PlayOneShot(equip, volume);
    }
    public void Maltiply(){
        source.PlayOneShot(maltiply, volume);
    }
}
