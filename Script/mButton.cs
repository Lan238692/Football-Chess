using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//This is the code corresponding to the game's UI buttons

public class mButton : MonoBehaviour
{
    public string buttonType;
    public GameObject canvas;
    public Transform[] status;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        
        if (buttonType == "Exit")
        {
            Debug.Log ("Game exit");
            Application.Quit();
        }
        if(buttonType == "Start")
        {
            Debug.Log("Game start");
            canvas.SetActive(false);
        }
        if(buttonType == "StartAI")
        {
            Global.isHavingAI = true;
            Debug.Log("Game start(AI mode)");
            canvas.SetActive(false);
        }
        if(buttonType == "Help")
        {
            GameObject.Find("HelpVision").transform.localPosition = new Vector3(0f, 0f, 0f);           
            GameObject.Find("StatusCanvasPos").transform.localPosition += Global.bigDelta;
            GameObject.Find("Exit").transform.localPosition += Global.bigDelta;
            GameObject.Find("Help").transform.localPosition += Global.bigDelta;
            GameObject.Find("Restart").transform.localPosition += Global.bigDelta;
        }
        if(buttonType == "CloseHelp")
        {
            GameObject.Find("HelpVision").transform.localPosition = Global.bigDelta;
            GameObject.Find("StatusCanvasPos").transform.localPosition -= Global.bigDelta;
            GameObject.Find("Exit").transform.localPosition -= Global.bigDelta;
            GameObject.Find("Help").transform.localPosition -= Global.bigDelta;
            GameObject.Find("Restart").transform.localPosition -= Global.bigDelta;
        }
        if(buttonType == "Retry")
        {
            GameObject.Find("FormationPos").transform.localPosition += Global.bigDelta;
            GameObject.Find("ResultVisionPos").transform.localPosition += Global.bigDelta;         
        }
        if(buttonType == "Restart")
        {
            GameObject.Find("FormationPos").transform.localPosition += Global.bigDelta;
            GameObject.Find("ResultVisionPos").transform.localPosition += Global.bigDelta;
            GameObject.Find("StatusCanvasPos").transform.localPosition += Global.bigDelta;
            GameObject.Find("Exit").transform.localPosition += Global.bigDelta;
            GameObject.Find("Help").transform.localPosition += Global.bigDelta;
            GameObject.Find("Restart").transform.localPosition += Global.bigDelta;
        }

        //Set the position of players in different formations
        if(buttonType == "442")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-2");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-2");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-4");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-4");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-6");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-6");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-8");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-8");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-2");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-2");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-4");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-4");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-6");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-6");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-8");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-8");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-4");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-4");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-6");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-6");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
        if(buttonType == "433")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-2");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-2");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-4");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-4");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-6");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-6");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-8");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-8");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-3");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-3");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-5");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-5");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-7");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-7");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-2");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-2");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-8");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-8");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-5");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
        if(buttonType == "4231")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-2");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-2");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-4");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-4");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-6");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-6");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-8");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-8");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-3");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-3");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-4");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-4");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-6");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-6");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-5");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-5");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-7");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-7");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-5");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
        if(buttonType == "4141")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-2");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-2");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-4");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-4");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-6");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-6");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-8");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-8");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos3-5");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos10-5");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-2");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-2");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-4");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-4");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-6");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-6");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-8");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-8");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-5");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
        if(buttonType == "3412")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-3");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-3");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-5");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-5");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-7");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-7");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos5-5");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos8-5");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-2");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-2");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-4");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-4");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-6");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-6");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-8");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-8");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-4");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-4");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-6");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-6");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
        if(buttonType == "532")
        {    
            Global.homeTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-3");
            Global.homeTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-3");
            Global.homeTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-5");
            Global.homeTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-5");
            Global.homeTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos2-7");
            Global.homeTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos11-7");
            Global.homeTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos3-1");
            Global.homeTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos10-1");
            Global.homeTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos3-9");
            Global.homeTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos10-9");
            Global.homeTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-3");
            Global.homeTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-3");
            Global.homeTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-5");
            Global.homeTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-5");
            Global.homeTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos4-7");
            Global.homeTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos9-7");
            Global.homeTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-4");
            Global.homeTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-4");
            Global.homeTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos6-6");
            Global.homeTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos7-6");
            Global.homeTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos1-5");
            Global.homeTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos12-5");
            Global.firstHalfKickoff = true;
            gameInitial();  
        }
	}
    void gameInitial()
    {
        //away player position is ‘default’ temporarily
        Global.awayTeamPlayer[0].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos11-2");
        Global.awayTeamPlayer[0].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos2-2");
        Global.awayTeamPlayer[1].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos11-4");
        Global.awayTeamPlayer[1].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos2-4");
        Global.awayTeamPlayer[2].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos11-6");
        Global.awayTeamPlayer[2].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos2-6");
        Global.awayTeamPlayer[3].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos11-8");
        Global.awayTeamPlayer[3].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos2-8");
        Global.awayTeamPlayer[4].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos9-2");
        Global.awayTeamPlayer[4].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos4-2");
        Global.awayTeamPlayer[5].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos9-4");
        Global.awayTeamPlayer[5].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos4-4");
        Global.awayTeamPlayer[6].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos9-6");
        Global.awayTeamPlayer[6].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos4-6");
        Global.awayTeamPlayer[7].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos9-8");
        Global.awayTeamPlayer[7].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos4-8");
        Global.awayTeamPlayer[8].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos7-4");
        Global.awayTeamPlayer[8].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos6-4");
        Global.awayTeamPlayer[9].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos7-6");
        Global.awayTeamPlayer[9].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos6-6");
        Global.awayTeamPlayer[10].GetComponent<Player>().firstHalfStartPos = GameObject.Find("Pos12-5");
        Global.awayTeamPlayer[10].GetComponent<Player>().secondHalfStartPos = GameObject.Find("Pos1-5");
        //prepare for starting
        Global.half = false;
        Global.side = false;
        Global.gameTime = 0;
        Global.leftPoint = Global.movePoint = Global.defaultPoint;
        Global.homeScore = 0;
        Global.awayScore = 0;
        Global.homeEdge.SetActive(true);
        Global.awayEdge.SetActive(false);
        GameObject.Find("StatusCanvasPos").transform.localPosition -= Global.bigDelta;
        GameObject.Find("FormationPos").transform.localPosition -= Global.bigDelta;
        GameObject.Find("Exit").transform.localPosition -= Global.bigDelta;
        GameObject.Find("Help").transform.localPosition -= Global.bigDelta;
        GameObject.Find("Restart").transform.localPosition -= Global.bigDelta;
        Global.isWorking = false;
        Global.playWhistle = true;
    }
}