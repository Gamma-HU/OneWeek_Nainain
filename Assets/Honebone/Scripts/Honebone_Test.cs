using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honebone_Test : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    public void Click()
    {
        particle.Play();
    }
}
