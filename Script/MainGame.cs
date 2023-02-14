using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainGame : MonoBehaviour
{
    public Transform prefab;
    public GameObject myBall;
    public GameObject[] floor;
    public GameObject[] player;
    public GameObject homeEdge;
    public GameObject awayEdge;
    public GameObject leftGKPosition;
    public GameObject rightGKPosition;
    public GameObject leftKickoffPos;
    public GameObject rightKickoffPos;
    public GameObject leftKickoffPos2;
    public GameObject rightKickoffPos2;
    public float acc = -0.5f;
    public float playerSpeed = 5f;
    public float ballSpeed = 10f;
    public float shootSpeed = 30f;
    public float slowBallSpeed = 6f;
    public bool exe2times = false;


    // Start is called before the first frame update
    void Start()
    {
        //Initializing the game, which consists of two parts: hiding menus that should not be displayed
        //and initializing data such as the score.

        Global.isWorking = true;
        GameObject.Find("StatusCanvasPos").transform.localPosition += Global.bigDelta;
        GameObject.Find("HelpVision").transform.localPosition += Global.bigDelta;
        GameObject.Find("Exit").transform.localPosition += Global.bigDelta;
        GameObject.Find("Help").transform.localPosition += Global.bigDelta;
        GameObject.Find("Restart").transform.localPosition += Global.bigDelta;
        GameObject.Find("ResultVisionPos").transform.localPosition += Global.bigDelta;

        Global.half = false;
        Global.side = false;
        Global.gameTime = 0;
        Global.leftPoint = Global.movePoint = Global.defaultPoint;
        Global.homeScore = 0;
        Global.awayScore = 0;
        Global.homeEdge = homeEdge;
        Global.homeEdge.SetActive(true);
        Global.awayEdge = awayEdge;
        Global.awayEdge.SetActive(false);
        Global.leftGKPosition = leftGKPosition;
        Global.rightGKPosition = rightGKPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Handling when a side runs out of action points
        if (Global.leftPoint == 0)
        {
            Global.side = !Global.side;

            Global.turn += 1;
            if(Global.turn % 2 == 1)
            {
                Global.defaultPoint++;
            }
            Global.movePoint = Global.defaultPoint;
            Global.leftPoint = Global.defaultPoint;
            Global.gameTime += Global.timePerTurn;
            if(Global.gameTime == 45)
            {
                Global.isWorking = true;
                Global.turn = 0;
                Global.leftPoint = Global.movePoint = Global.defaultPoint = 4;
                //show the logo of "HALFTIME!!!" for Global.logoLastTime
                Global.logoMessage = "HALFTIME";
                Global.logoTimer = Global.logoLastTime;
            }
            if(Global.gameTime == 90)
            {
                Global.isWorking = true;
                Global.turn = 0;
                Global.leftPoint = Global.movePoint = Global.defaultPoint = 4;
                //show the logo of result for Global.logoLastTime
                if(Global.homeScore > Global.awayScore)
                    Global.logoMessage = "HOMEWIN";
                if(Global.homeScore < Global.awayScore)
                    Global.logoMessage = "AWAYWIN";
                if(Global.homeScore == Global.awayScore)
                    Global.logoMessage = "DRAW";
                Global.logoTimer = Global.logoLastTime;
            }
            if(Global.side == true)
            {
                homeEdge.SetActive(false);
                awayEdge.SetActive(true);
            }
            else
            {
                homeEdge.SetActive(true);
                awayEdge.SetActive(false);
            }
            
        }

        //When the players' positions have been reset, proceed to the next step
        if(Global.isPositionChanged >= 22 && exe2times == false)
        {
            Global.isPositionChanged = 0;
            exe2times = true;
        }
        if(Global.isPositionChanged >= 22 && exe2times == true)
        {
            Global.secondHalfKickoff = Global.firstHalfKickoff = false;
            Global.isPositionChanged = 0;
            exe2times = false;
            if(Global.half == Global.side)
            {
                if(leftKickoffPos.GetComponent<Position>().standPlayer!=null)
                {
                    myBall.transform.localPosition = leftKickoffPos.transform.localPosition + new Vector3(0.4f, 0f, 0.5f);
                    leftKickoffPos.GetComponent<Position>().standBall = true;
                    leftKickoffPos2.GetComponent<Position>().standBall = false;
                    rightKickoffPos.GetComponent<Position>().standBall = false;
                    rightKickoffPos2.GetComponent<Position>().standBall = false;
                }
                else
                {
                    myBall.transform.localPosition = leftKickoffPos2.transform.localPosition + new Vector3(0.4f, 0f, 0.5f);
                    leftKickoffPos.GetComponent<Position>().standBall = false;
                    leftKickoffPos2.GetComponent<Position>().standBall = true;
                    rightKickoffPos.GetComponent<Position>().standBall = false;
                    rightKickoffPos2.GetComponent<Position>().standBall = false;
                }
            }
            else
            {
                if(rightKickoffPos.GetComponent<Position>().standPlayer!=null)
                {
                    myBall.transform.localPosition = rightKickoffPos.transform.localPosition + new Vector3(0.4f, 0f, -0.5f);
                    leftKickoffPos.GetComponent<Position>().standBall = false;
                    leftKickoffPos2.GetComponent<Position>().standBall = false;
                    rightKickoffPos.GetComponent<Position>().standBall = true;
                    rightKickoffPos2.GetComponent<Position>().standBall = false;
                }
                else
                {
                    myBall.transform.localPosition = rightKickoffPos2.transform.localPosition + new Vector3(0.4f, 0f, -0.5f);
                    leftKickoffPos.GetComponent<Position>().standBall = false;
                    leftKickoffPos2.GetComponent<Position>().standBall = false;
                    rightKickoffPos.GetComponent<Position>().standBall = false;
                    rightKickoffPos2.GetComponent<Position>().standBall = true;
                }
            }
            Global.isWorking = false;
        }

        //Handling the actual action of shooting
        if(Global.isReadyToShoot == true)
        {
            Global.isPlayerSelected = Global.isToShoot = false;
            Global.isWorking = true;
            var ballStep = shootSpeed * Time.deltaTime;
            var ballDesPos = Global.desPos;
            myBall.transform.localPosition = Vector3.MoveTowards(myBall.transform.localPosition, ballDesPos, ballStep);
            if(Vector3.Distance(myBall.transform.localPosition, ballDesPos)<0.001f)
            {
                myBall.transform.localPosition = ballDesPos;
                Global.isReadyToShoot = false;              

                //reset
                if(Global.isGoal == false)
                {
                    Global.movePoint = 0;
                    Global.isWorking = false;
                    Global.restorePlayerRotation(Global.player);
                    Global.player = Global.position = null;
                    Global.leftPoint = Global.movePoint;
                }
                if(Global.isGoal == true && Global.gameTime != 45 - Global.timePerTurn && Global.gameTime != 90 - Global.timePerTurn)
                {
                    if(Global.side == false)
                        Global.homeScore++;
                    else
                        Global.awayScore++;
                    Global.side = !Global.side;
                    Global.turn = 0;
                    Global.defaultPoint = 4;
                    Global.movePoint = Global.defaultPoint;
                    Global.leftPoint = Global.defaultPoint;
                    Global.gameTime += Global.timePerTurn;
                    if(Global.side == true)
                    {
                        homeEdge.SetActive(false);
                        awayEdge.SetActive(true);
                    }
                    else
                    {
                        homeEdge.SetActive(true);
                        awayEdge.SetActive(false);
                    }

                    //add logo of "GOAL!!!" for logoLastTime
                    Global.logoMessage = "GOAL";
                    Global.logoMessage2 = true;
                    Global.logoTimer = Global.logoLastTime;
                    Global.isGoal = false;
                }
                else if(Global.isGoal == true)
                {
                    if(Global.side == false)
                        Global.homeScore++;
                    else
                        Global.awayScore++;
                    Global.movePoint = 0;
                    Global.turn = 0;
                    Global.defaultPoint = 4;
                    //add logo of "GOAL!!!" for logoLastTime
                    Global.logoMessage = "GOAL";
                    Global.logoTimer = Global.logoLastTime;
                    Global.isGoal = false;
                }
            }
        }

        //Handling the actual action of moving
        if(Global.isReadyToMove == true)
        {
            Global.isPlayerSelected = Global.isToMove = false;
            Global.isWorking = true;
            var playerStep = playerSpeed * Time.deltaTime;
            Global.player.transform.localPosition = Vector3.MoveTowards(Global.player.transform.localPosition, Global.desPosition.transform.localPosition, playerStep);
            if(Global.isDribble == true)
            {
                Global.dribbledPlayer.transform.localPosition = Vector3.MoveTowards(Global.dribbledPlayer.transform.localPosition, Global.srcPos, playerStep);
                if(Vector3.Distance(Global.dribbledPlayer.transform.localPosition, Global.srcPos)<0.001f)
                {
                    Global.dribbledPlayer.transform.localPosition = Global.srcPos;
                    Global.dribbledPlayer.GetComponent<Player>().anime.Play("rig|stand");
                    Global.restorePlayerRotation(Global.dribbledPlayer, true);
                }
            }       
            if(Global.isPlayerHoldsBall == true)
            {
                var ballStep = ballSpeed * Time.deltaTime;
                var ballDesPos = Global.desPosition.transform.localPosition;
                if(Global.side == Global.half)
                    ballDesPos += new Vector3(0.4f, 0f, 0.5f);
                else
                    ballDesPos += new Vector3(0.4f, 0f, -0.5f);
                myBall.transform.localPosition = Vector3.MoveTowards(myBall.transform.localPosition, ballDesPos, ballStep);
                if(Vector3.Distance(myBall.transform.localPosition, ballDesPos)<0.001f)
                {
                    myBall.transform.localPosition = ballDesPos;
                }
            }
            else if(Global.desPosition.GetComponent<Position>().standBall == true)
            {
                if(Vector3.Distance(Global.player.transform.localPosition,Global.desPosition.transform.localPosition)<0.8f)
                {
                    var ballStep = ballSpeed * Time.deltaTime;
                    var ballDesPos = Global.desPosition.transform.localPosition;
                    if(Global.side == Global.half)
                        ballDesPos += new Vector3(0.4f, 0f, 0.5f);
                    else
                        ballDesPos += new Vector3(0.4f, 0f, -0.5f);
                    myBall.transform.localPosition = Vector3.MoveTowards(myBall.transform.localPosition, ballDesPos, ballStep);
                    if(Vector3.Distance(myBall.transform.localPosition, ballDesPos)<0.001f)
                    {
                        myBall.transform.localPosition = ballDesPos;
                    }
                }
            }
            if(Vector3.Distance(Global.player.transform.localPosition, Global.desPosition.transform.localPosition)<0.001f)
            {
                Debug.Log("reach target position");
                Global.player.transform.localPosition = Global.desPosition.transform.localPosition;
                Global.player.GetComponent<Player>().anime.Play("rig|stand");
                Global.restorePlayerRotation(Global.player);
                Global.isReadyToMove = false;
                Global.isWorking = false;
                Global.leftPoint = Global.movePoint;
                Global.player = Global.position = null;

                if(Global.isDribbleGK == true)
                {
                    if(Global.side == Global.half)
                    {
                        Global.desPos = Global.right3;
                    }
                    else
                    {
                        Global.desPos = Global.left3;
                    }
                    Global.isGoal = true;
                    Global.isReadyToShoot = true;
                    Global.isDribbleGK = false;
                }
            }
        }

        //Handling the actual action of passing
        if(Global.isReadyToPass == true)
        {
            Global.isPlayerSelected = Global.isToPass = false;
            Global.isWorking = true;
            var ballStep = ballSpeed * Time.deltaTime;
            var ballDesPos = Global.desPosition.transform.localPosition;
            if(Global.desPosition.GetComponent<Position>().standPlayer != null)
            {
                if(Global.side == Global.half)
                    ballDesPos +=  new Vector3(0.4f, 0f, 0.5f);
                else
                    ballDesPos +=  new Vector3(0.4f, 0f, -0.5f);
            }
            myBall.transform.localPosition = Vector3.MoveTowards(myBall.transform.localPosition, ballDesPos, ballStep);
            if(Vector3.Distance(myBall.transform.localPosition, ballDesPos)<0.001f)
            {
                myBall.transform.localPosition = ballDesPos;
                Global.isReadyToPass = false;
                Global.isWorking = false;
                Global.leftPoint = Global.movePoint;
                Global.restorePlayerRotation(Global.player);
                Global.player = Global.position = null;              
            }
        }
    }
}
