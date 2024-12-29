using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConbineUI : MonoBehaviour
{
  Image image;
  void Start(){
    image = GetComponent<Image>();
  }
  public void Invisible(){
    image.enabled = false;
  }

}
