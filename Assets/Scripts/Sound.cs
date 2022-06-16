using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip running;

    private float x, z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = GameObject.Find("Player").GetComponent<PlayerMovement>().x;
        z = GameObject.Find("Player").GetComponent<PlayerMovement>().z;

        if(x != 0 || z != 0)
        {
            source.PlayOneShot(running);
        }
        else
        {
            source.Stop();
        }
    }
}
