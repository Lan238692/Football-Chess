using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the timer used to show/hide the prompt bar corresponding to the player action.

public class ButtonTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.buttonTimer>=0)
        {           
            GameObject.Find("RightUp").transform.localPosition = Global.RightUpPos;
            GameObject.Find("RightUpText").transform.localPosition = Global.RightUpTextPos;
            GameObject.Find("RightUpData").transform.localPosition = Global.RightUpDataPos;
            Global.buttonTimer -= Time.deltaTime;
        }

        if(Global.shootButtonTimer>=0)
        {           
            GameObject.Find("RightUp").transform.localPosition = Global.RightUpPos;
            GameObject.Find("RightUpText").transform.localPosition = Global.RightUpTextPos;
            GameObject.Find("RightUpData").transform.localPosition = Global.RightUpDataPos;
            Global.shootButtonTimer -= Time.deltaTime;
        }
        
        if(Global.buttonTimer < 0 && Global.shootButtonTimer < 0)
        {
            GameObject.Find("RightUp").transform.localPosition = Global.hide;
            GameObject.Find("RightUpText").transform.localPosition = Global.hide;
            GameObject.Find("RightUpData").transform.localPosition = Global.hide;
        }
    }
}
