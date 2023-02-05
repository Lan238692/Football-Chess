using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource mWhistle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.playWhistle == true)
        {
            mWhistle.Play();
            Global.playWhistle = false;
        }
        
    }
}
