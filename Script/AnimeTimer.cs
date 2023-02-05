using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Animation timer. It is used to handle the problem that there is a delay between actions.

public class AnimeTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.kickTimer >= 0f)
        {
            Global.kickTimer -= Time.deltaTime;
        }
        if(Global.runTimer >= 0f)
        {
            Global.kickTimer -= Time.deltaTime;
        }
    }
}
