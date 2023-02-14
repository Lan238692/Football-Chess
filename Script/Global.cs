using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Carrying global variables and functions

public class Global : MonoBehaviour
{

    public static bool isHavingAI = false;

    //match status

    //When a player has finished a turn, 'timePerTurn' minutes will pass
    public static int timePerTurn = 3;
    public static int gameTime = 0;

    
    //side means which side is ready to move 
    //side = home team(0) or away team(1)
    public static bool side = false;
    //side = first half(0) or second half(1)
    public static bool half = false;

    //Each turn the player's action points(movePoint & leftPoint) go up with the number of turns(turn)
    //and revert to their initial value(defaultPoint) when one side scores or goes into the second half.
    public static int defaultPoint = 4;
    public static int turn = 0;
    public static int movePoint;
    public static int leftPoint;

    //match score
    public static int homeScore = 0;
    public static int awayScore = 0;

    //player arrays
    public static GameObject[] homeTeamPlayer;
    public static GameObject[] awayTeamPlayer;

    //This is the kickoff signal, and when they are set to true, the player and ball positions are reset
    public static bool firstHalfKickoff = false;
    public static bool secondHalfKickoff = false;
    public static int isPositionChanged = 0;

    //Used to indicate whose turn it is.
    public static GameObject homeEdge;
    public static GameObject awayEdge;

    //goal position(2 positions for each goal)
    public static Vector3 left1 = new Vector3(1.054f, 0.1f, -12.874f);
    public static Vector3 left2 = new Vector3(-1.054f, 0.1f, -12.874f);
    public static Vector3 left3 = new Vector3(0f, 0.1f, -12.874f);
    public static Vector3 right1 = new Vector3(1.054f, 0.1f, 12.874f);
    public static Vector3 right2 = new Vector3(-1.054f, 0.1f, 12.874f);
    public static Vector3 right3 = new Vector3(0f, 0.1f, 12.874f);

    //goalball position
    public static Vector3 leftGoalball = new Vector3(0f, 0.1f, -10.6f);
    public static Vector3 rightGoalball = new Vector3(0f, 0.1f, 10.6f);
    public static GameObject leftGKPosition;
    public static GameObject rightGKPosition;

    //If the player or the ball is running, used to avoid multiple operations happening at the same time.
    public static bool isWorking = false;

    //if any button is pressed
    public static bool isPassButtonPressed = false;
    public static bool isShootButtonPressed = false;
    public static bool isMoveButtonPressed = false;

    public static bool isPlayerHoldsBall = false;

    //player menu
    public static bool isMenuOpened = false;

    //Used to record the starting and target locations and players when passing/moving the ball.
    public static Vector3 srcPos;
    public static Vector3 desPos;
    public static GameObject player;
    public static GameObject position;
    public static GameObject dribbledPlayer;
    public static GameObject desPosition;
    public static GameObject football;

    public static bool isPlayerSelected = false;
    public static bool isToMove = false;
    public static bool isToPass = false;
    public static bool isToShoot = false;
    public static bool isReadyToMove = false;
    public static bool isDribble = false;
    public static bool isReadyToPass = false;
    public static bool isReadyToShoot = false;
    public static bool isGoal = false;
    public static bool isDribbleGK = false;

    //These two large numbers are used to move unwanted menus out of sight.
    public static Vector3 hide = new Vector3(-10000, -10000, -10000);
    public static Vector3 bigDelta = new Vector3(-100000, -100000, -100000);

    //Responsible for managing UI objects.
    public static string message;
    public static float timer = 0f;

    public static string logoMessage;
    public static bool logoMessage2 = false;
    public static float logoTimer = -1.0f;
    public static float logoLastTime = 10.0f;

    public static GameObject picGOAL;
    public static GameObject picHALFTIME;
    public static GameObject picHOMEWIN;
    public static GameObject picAWAYWIN;
    public static GameObject picDRAW;

    public static Vector3 RightUpPos;
    public static Vector3 RightUpTextPos;
    public static Vector3 RightUpDataPos;
    public static float buttonTimer = 0f;
    public static float shootButtonTimer = 0f;

    //music playback switch
    public static bool playWhistle = false;
    public static bool playShootAnime = false;

    //anime timer
    public static float kickTimer = 0f;
    public static float runTimer = 0f;
    public static float preKickTime = 0.8f;


    //Responsible for adjusting the player's orientation during operation.
    public static void adjustPlayerRotationShoot(GameObject mPlayer)
    {
        if(half == side)
        {
            football.transform.localPosition = mPlayer.transform.localPosition;
            if(mPlayer.transform.localPosition.x < 0)
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, 40f, 0f);
                football.transform.localPosition += new Vector3(0.4f, 0f, 0.5f);
            }
            else if(mPlayer.transform.localPosition.x == 0)
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                football.transform.localPosition += new Vector3(0f, 0f, 0.6f);
            }
            else
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, -40f, 0f);
                football.transform.localPosition += new Vector3(-0.4f, 0f, 0.5f);
            }
        }
        else
        {
            football.transform.localPosition = mPlayer.transform.localPosition;
            if(mPlayer.transform.localPosition.x < 0)
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, 140f, 0f);
                football.transform.localPosition += new Vector3(0.4f, 0f, -0.5f);
            }
            else if(mPlayer.transform.localPosition.x == 0)
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                football.transform.localPosition += new Vector3(0f, 0f, -0.6f);
            }
            else
            {
                mPlayer.transform.localEulerAngles = new Vector3(0f, 220f, 0f);
                football.transform.localPosition += new Vector3(-0.4f, 0f, -0.5f);
            }
        }
    }
    public static void adjustPlayerRotationPass(GameObject mPlayer, GameObject des)
    {
        football.transform.localPosition = mPlayer.transform.localPosition;
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 40f, 0f);
            football.transform.localPosition += new Vector3(0.4f, 0f, 0.5f);
        }
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z == des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            football.transform.localPosition += new Vector3(0.4f, 0f, 0f);
        }
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 140f, 0f);
            football.transform.localPosition += new Vector3(0.4f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x == des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            football.transform.localPosition += new Vector3(0f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 220f, 0f);
            football.transform.localPosition += new Vector3(-0.4f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z == des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
            football.transform.localPosition += new Vector3(-0.4f, 0f, 0f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 320f, 0f);
            football.transform.localPosition += new Vector3(-0.4f, 0f, 0.5f);
        }
        if(mPlayer.transform.localPosition.x == des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            football.transform.localPosition += new Vector3(0f, 0f, 0.5f);
        }
    }
    public static void adjustPlayerRotationMove(GameObject mPlayer,GameObject des, bool haveBall)
    {
        if(haveBall) football.transform.localPosition = mPlayer.transform.localPosition;
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 40f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(0.4f, 0f, 0.5f);
        }
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z == des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(0.4f, 0f, 0f);
        }
        if(mPlayer.transform.localPosition.x < des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 140f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(0.4f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x == des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(0f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z > des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 220f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(-0.4f, 0f, -0.5f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z == des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(-0.4f, 0f, 0f);
        }
        if(mPlayer.transform.localPosition.x > des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 320f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(-0.4f, 0f, 0.5f);
        }
        if(mPlayer.transform.localPosition.x == des.transform.localPosition.x
            &&mPlayer.transform.localPosition.z < des.transform.localPosition.z)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            if(haveBall) football.transform.localPosition += new Vector3(0f, 0f, 0.5f);
        }
    }
    public static void restorePlayerRotation(GameObject mPlayer, bool reverse = false)
    {
        if(side == half)
        {
            if(reverse == false)
                mPlayer.transform.localEulerAngles = new Vector3(0f, 40f, 0f);
            else
                mPlayer.transform.localEulerAngles = new Vector3(0f, 140f, 0f);
        }
        else
        {
            if(reverse == false)
                mPlayer.transform.localEulerAngles = new Vector3(0f, 140f, 0f);
            else
                mPlayer.transform.localEulerAngles = new Vector3(0f, 40f, 0f);
        }
    }

    public static bool isDistanceOne(GameObject pos1, GameObject pos2)
    {
        if(Vector3.Distance(pos1.transform.localPosition, pos2.transform.localPosition)<3.0f)
            return true;
        else
            return false;
    }
    public static float max(float f1, float f2, float f3, float f4)
    {
        float f = f1;
        if(f2 > f) f = f2;
        if(f3 > f) f = f3;
        if(f4 > f) f = f4;
        return f;
    }
    public static float min(float f1, float f2, float f3, float f4)
    {
        float f = f1;
        if(f2 < f) f = f2;
        if(f3 < f) f = f3;
        if(f4 < f) f = f4;
        return f;
    }
    public static void swap(GameObject g1, GameObject g2)
    {
        var temp = g1;
        g1 = g2;
        g2 = temp;
    }

    //show/hide the game object(UI)
    public static void hideObject(GameObject obj)
    {
        obj.transform.localPosition += bigDelta;

    }
    public static void showObject(GameObject obj)
    {
        obj.transform.localPosition -= bigDelta;
    }

    //count that how many players are between the shooting player and the goal
    //the more players blocked, the lower the goal success rate will be
    public static int countPlayer(bool side, bool toLeftOrRight)
    {
        int count = 0;
        if(Global.player == null)
        {
            Debug.Log("countPlayerError!");
            return count;
        }
        
        if((Global.player.transform.localPosition.z == -11.09f || Global.player.transform.localPosition.z == 11.09f) && Global.player.transform.localPosition.x > 0)
        {
            if(side == false)
            {
                for(int i = 0; i < 10;i++)
                {
                    if(awayTeamPlayer[i].transform.localPosition.z == Global.player.transform.localPosition.z
                    && awayTeamPlayer[i].transform.localPosition.x > 0 
                    && awayTeamPlayer[i].transform.localPosition.x < Global.player.transform.localPosition.x)
                        count++;
                }
                return count;
            }
            else
            {
                for(int i = 0; i < 10;i++)
                {
                    if(homeTeamPlayer[i].transform.localPosition.z == Global.player.transform.localPosition.z
                    && homeTeamPlayer[i].transform.localPosition.x > 0 
                    && homeTeamPlayer[i].transform.localPosition.x < Global.player.transform.localPosition.x)
                        count++;
                }
                return count;
            }
        }
        if((Global.player.transform.localPosition.z == -11.09f || Global.player.transform.localPosition.z == 11.09f) && Global.player.transform.localPosition.x < 0)
        {
            if(side == false)
            {
                for(int i = 0; i < 10;i++)
                {
                    if(awayTeamPlayer[i].transform.localPosition.z == Global.player.transform.localPosition.z
                    && awayTeamPlayer[i].transform.localPosition.x < 0 
                    && awayTeamPlayer[i].transform.localPosition.x > Global.player.transform.localPosition.x)
                        count++;
                }
                return count;
            }
            else
            {
                for(int i = 0; i < 10;i++)
                {
                    if(homeTeamPlayer[i].transform.localPosition.z == Global.player.transform.localPosition.z
                    && homeTeamPlayer[i].transform.localPosition.x < 0 
                    && homeTeamPlayer[i].transform.localPosition.x > Global.player.transform.localPosition.x)
                        count++;
                }
                return count;
            }
        }

        float delta = 0.8f;
        Vector3 leftPos = new Vector3(0f, 0.3f, -11.09f);
        Vector3 rightPos = new Vector3(0f, 0.3f, 11.09f);
        Vector3 playerPos = Global.player.transform.localPosition;

        Vector3 p1 = leftPos + new Vector3(delta, 0, delta);
        Vector3 p2 = leftPos + new Vector3(delta, 0, -delta);
        Vector3 p3 = leftPos + new Vector3(-delta, 0, delta);
        Vector3 p4 = leftPos + new Vector3(-delta, 0, -delta);
        Vector3 p1d = playerPos + new Vector3(delta, 0, delta);
        Vector3 p2d = playerPos + new Vector3(delta, 0, -delta);
        Vector3 p3d = playerPos + new Vector3(-delta, 0, delta);
        Vector3 p4d = playerPos + new Vector3(-delta, 0, -delta);
        float gra = (p1.x-p1d.x)/(p1.z-p1d.z);
        float int1 = p1.x - p1.z * gra;
        float int2 = p2.x - p2.z * gra;
        float int3 = p3.x - p3.z * gra;
        float int4 = p4.x - p4.z * gra;
        for(int i = 0; i < 10; i++)
        {
            Vector3 tempPos;
            if(side == false)
                tempPos = awayTeamPlayer[i].transform.localPosition;
            else
                tempPos = homeTeamPlayer[i].transform.localPosition;
            if(toLeftOrRight == false && tempPos.z > playerPos.z)
                continue;
            if(toLeftOrRight == true && tempPos.z < playerPos.z)
                continue;
            float tempInt = tempPos.x - tempPos.z * gra;
            if(tempInt < max(int1, int2, int3, int4) && tempInt > min(int1, int2, int3, int4))
                count++;
        }
        return count;


    }


    void Start()
    {
        homeTeamPlayer = new GameObject[11];
        awayTeamPlayer = new GameObject[11];
        
        homeTeamPlayer[0] = GameObject.Find("HomePlayer1");
        homeTeamPlayer[1] = GameObject.Find("HomePlayer2");
        homeTeamPlayer[2] = GameObject.Find("HomePlayer3");
        homeTeamPlayer[3] = GameObject.Find("HomePlayer4");
        homeTeamPlayer[4] = GameObject.Find("HomePlayer5");
        homeTeamPlayer[5] = GameObject.Find("HomePlayer6");
        homeTeamPlayer[6] = GameObject.Find("HomePlayer7");
        homeTeamPlayer[7] = GameObject.Find("HomePlayer8");
        homeTeamPlayer[8] = GameObject.Find("HomePlayer9");
        homeTeamPlayer[9] = GameObject.Find("HomePlayer10");
        homeTeamPlayer[10] = GameObject.Find("HomePlayerGK");

        awayTeamPlayer[0] = GameObject.Find("AwayPlayer1");
        awayTeamPlayer[1] = GameObject.Find("AwayPlayer2");
        awayTeamPlayer[2] = GameObject.Find("AwayPlayer3");
        awayTeamPlayer[3] = GameObject.Find("AwayPlayer4");
        awayTeamPlayer[4] = GameObject.Find("AwayPlayer5");
        awayTeamPlayer[5] = GameObject.Find("AwayPlayer6");
        awayTeamPlayer[6] = GameObject.Find("AwayPlayer7");
        awayTeamPlayer[7] = GameObject.Find("AwayPlayer8");
        awayTeamPlayer[8] = GameObject.Find("AwayPlayer9");
        awayTeamPlayer[9] = GameObject.Find("AwayPlayer10");
        awayTeamPlayer[10] = GameObject.Find("AwayPlayerGK");
        
        
        RightUpPos = GameObject.Find("RightUp").transform.localPosition;
        RightUpTextPos = GameObject.Find("RightUpText").transform.localPosition;
        RightUpDataPos = GameObject.Find("RightUpData").transform.localPosition;
        GameObject.Find("RightUp").transform.localPosition = Global.hide;
        GameObject.Find("RightUpText").transform.localPosition = Global.hide;
        GameObject.Find("RightUpData").transform.localPosition = Global.hide;

        GameObject mParent = GameObject.Find("Logo");
        picGOAL = mParent.transform.Find("GOAL").gameObject;
        picHALFTIME = mParent.transform.Find("HALFTIME").gameObject;
        picHOMEWIN = mParent.transform.Find("HOMEWIN").gameObject;
        picAWAYWIN = mParent.transform.Find("AWAYWIN").gameObject;
        picDRAW = mParent.transform.Find("DRAW").gameObject;

        football = GameObject.Find("SoccerBall");
    }
    void Update()
    {

    }
}
