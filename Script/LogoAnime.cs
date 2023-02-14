using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used to pop up game information such as "GOAL!!!" "HALF TIME!!!"

public class LogoAnime : MonoBehaviour
{
    public string description;
    public Vector3 pos;
    public bool exeOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.localPosition;
        this.transform.localPosition = Global.hide;
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.logoTimer >= 0f)
        {
            Global.isWorking = true;
            exeOnce = true;
            if(description == Global.logoMessage)
            {
                if(Global.logoMessage == "HOMEWIN" || Global.logoMessage == "AWAYWIN" || Global.logoMessage == "DRAW" || Global.logoMessage == "HALFTIME")
                {
                    Global.playWhistle = true;
                }
                this.transform.localPosition = pos;
                if(Global.logoTimer >= Global.logoLastTime - 3)
                    this.transform.localScale = new Vector3((Global.logoLastTime - Global.logoTimer)/3, (Global.logoLastTime - Global.logoTimer)/3, (Global.logoLastTime - Global.logoTimer)/3);
                else
                    this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            Global.logoTimer -= Time.deltaTime;
        }
        else
        {
            if(exeOnce == true)
            {
                exeOnce = false;
                if(Global.logoMessage == "GOAL" && Global.logoMessage == description)
                {
                    if(Global.logoMessage2 == true)
                    {
                        if(Global.gameTime<=45)
                            Global.firstHalfKickoff = true;
                        else
                            Global.secondHalfKickoff = true;
                        Global.logoMessage2 = false;
                    }
                    //Global.isWorking = false;
                    Global.player = Global.position = null;
                    Global.leftPoint = Global.movePoint;                    
                }
                if(Global.logoMessage == "HALFTIME" && Global.logoMessage == description)
                {
                    Global.half = true;
                    Global.secondHalfKickoff = true;
                }
                if((Global.logoMessage == "HOMEWIN" || Global.logoMessage == "AWAYWIN" || Global.logoMessage == "DRAW")&& Global.logoMessage == description)
                {
                    GameObject.Find("ResultText").GetComponent<Text>().text = description;
                    GameObject.Find("StatusCanvasPos").transform.localPosition += Global.bigDelta;
                    GameObject.Find("ResultVisionPos").transform.localPosition -= Global.bigDelta;
                    GameObject.Find("Exit").transform.localPosition += Global.bigDelta;
                    GameObject.Find("Help").transform.localPosition += Global.bigDelta;
                    GameObject.Find("Restart").transform.localPosition += Global.bigDelta;
                }
            }
            this.transform.localPosition = Global.hide;
        }
        
    }
}
