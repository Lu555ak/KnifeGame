using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }
    
}
