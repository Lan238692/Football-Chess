using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Key class to guide the interaction of players and the grid.

public class Position : MonoBehaviour
{
    public GameObject position;
    public GameObject light;

    public GameObject standPlayer;
    public bool standBall;

    public int cost;

    public bool passCountDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Triggers a ball movement event after the player has taken a foot for a period of time.
        if (passCountDown == true && Global.kickTimer <= 0)
        {
            Global.isReadyToPass = true;
            Global.movePoint -= cost;
            passCountDown = false;
        }

    }

    //When a move or pass command is triggered,
    //a circle of light will light up to indicate the target grid where the mouse is moving to.
    void OnMouseEnter()
    {
        if(Global.isWorking == true)
            return;
            
        if(Global.player != null)
            if(position == Global.player.GetComponent<Player>().playerPosition)
                return;
        
        if(Global.isToMove == true)
        {
            cost = 1;
            if(Global.isDistanceOne(Global.player, position) == false)
            {
                return;
            }
            if(position.GetComponent<Position>().standPlayer != null)
            {
                if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().mSide == Global.player.GetComponent<Player>().mSide)
                    return;
                if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().isGK == true)
                    return;
            }
            if(position.GetComponent<Position>().standPlayer != null)
            {
                if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().mSide != Global.player.GetComponent<Player>().mSide)
                    if(Global.position.GetComponent<Position>().standBall == true)
                        cost = 2;
            }

            GameObject.Find("RightUpText").GetComponent<TMP_Text>().text = "MOVE COST :";
            GameObject.Find("RightUpData").GetComponent<TMP_Text>().text = cost.ToString();
            Global.buttonTimer = 100.0f;

            light.transform.localPosition = position.transform.localPosition;
            light.SetActive(true);
            
        }
        if(Global.isToPass == true)
        {
            if(position.GetComponent<Position>().standPlayer == null)
                return;
            if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().mSide!=Global.player.GetComponent<Player>().mSide)
                return;
            
            if(Vector3.Distance(Global.position.transform.localPosition, position.transform.localPosition)<5.5f)
                cost = 1;
            else if(Vector3.Distance(Global.position.transform.localPosition, position.transform.localPosition)<9.5f)
                cost = 2;
            else
                cost = 3;
            GameObject.Find("RightUpText").GetComponent<TMP_Text>().text = "PASS COST :";
            GameObject.Find("RightUpData").GetComponent<TMP_Text>().text = cost.ToString();
            Global.buttonTimer = 100.0f;
            
            light.SetActive(true);
            light.transform.localPosition = position.transform.localPosition;
        }
    }
    void OnMouseExit()
    {
        Global.buttonTimer = 0f;
        light.SetActive(false);
    }

    void OnMouseDown()
    {
        if(Global.player != null)
            if(Global.player.GetComponent<Player>().playerPosition != null)
                if(position == Global.player.GetComponent<Player>().playerPosition)
                    return;

        if(Global.isToMove == true)
        {
            if(Global.isDistanceOne(position, Global.player) == false)
            {
                Global.message = "You can't move more than two squares at once!";
                Global.timer = 3.0f;
                Debug.Log("You can't move more than two squares at once!");
                Global.isPlayerSelected = false;
                Global.isToMove = false;
                Global.player = Global.position = null;
                Global.buttonTimer = 0f;
                light.SetActive(false);
                return;
            }
            Global.isDribble = false;
            if(position.GetComponent<Position>().standPlayer != null)
            {
                if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().mSide == Global.player.GetComponent<Player>().mSide)
                {
                    Global.message = "You can't move your player to your teammate!";
                    Global.timer = 3.0f;
                    Debug.Log("You can't move your player to your teammate!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = Global.position = null;
                    Global.buttonTimer = 0f;
                    light.SetActive(false);
                    return;
                }
                if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().isGK == true)
                {
                    Global.message = "You can't move your player to the goalkeeper!";
                    Global.timer = 3.0f;
                    Debug.Log("You can't move your player to the goalkeeper!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = Global.position = null;
                    Global.buttonTimer = 0f;
                    light.SetActive(false);
                    return;
                }
                if(Global.position.GetComponent<Position>().standBall == true && Global.movePoint < cost)
                {
                    Global.message = "Your point is not enough for dribbling!";
                    Global.timer = 3.0f;
                    Debug.Log("Your point is not enough for dribbling!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = Global.position = null;
                    Global.buttonTimer = 0f;
                    light.SetActive(false);
                    return;
                }
                Global.isDribble = true;
                Global.dribbledPlayer = position.GetComponent<Position>().standPlayer;
            }
            Global.desPosition = position;
            if(Global.isDribble == false)
            {
                position.GetComponent<Position>().standPlayer = Global.player;
                position.GetComponent<Position>().standPlayer.GetComponent<Player>().playerPosition.GetComponent<Position>().standPlayer = null;
                Global.movePoint -= 1;
            }
            else
            {
                position.GetComponent<Position>().standPlayer.GetComponent<Player>().playerPosition = Global.player.GetComponent<Player>().playerPosition;
                position.GetComponent<Position>().standPlayer = Global.player;
                position.GetComponent<Position>().standPlayer.GetComponent<Player>().playerPosition.GetComponent<Position>().standPlayer = Global.dribbledPlayer;
                Global.srcPos = Global.player.transform.localPosition;
                if(Global.isPlayerHoldsBall == true)
                    Global.movePoint -= 2;
                else
                    Global.movePoint -= 1;
                Global.adjustPlayerRotationMove(Global.dribbledPlayer, Global.player, false);
                Global.dribbledPlayer.GetComponent<Player>().anime.Play("rig|walk");
            }
            if(Global.isPlayerHoldsBall == true)
            {
                position.GetComponent<Position>().standPlayer.GetComponent<Player>().playerPosition.GetComponent<Position>().standBall = false;
                position.GetComponent<Position>().standBall = true;
            }
            position.GetComponent<Position>().standPlayer.GetComponent<Player>().playerPosition = position;
            Global.buttonTimer = 0f;
            Global.isReadyToMove = true;
            Global.adjustPlayerRotationMove(Global.player, Global.desPosition, Global.isPlayerHoldsBall);
            Global.player.GetComponent<Player>().anime.Play("rig|walk");
            light.SetActive(false);
        }
        if(Global.isToPass == true)
        {
            if(position.GetComponent<Position>().standPlayer == null)
            {
                Global.message = "You must pass the ball to a player of your own team!";
                Global.timer = 3.0f;
                Debug.Log("You must pass the ball to a player of your own team!");
                Global.isPlayerSelected = false;
                Global.isToPass = false;
                Global.player = Global.position = null;
                Global.buttonTimer = 0f;
                light.SetActive(false);
                return;
            }
            if(position.GetComponent<Position>().standPlayer.GetComponent<Player>().mSide != Global.player.GetComponent<Player>().mSide)
            {
                Global.message = "You can't pass the ball to the opposite side!";
                Global.timer = 3.0f;
                Debug.Log("You can't pass the ball to the opposite side!");
                Global.isPlayerSelected = false;
                Global.isToPass = false;
                Global.player = Global.position = null;
                Global.buttonTimer = 0f;
                light.SetActive(false);
                return;
            }
            if(Global.movePoint < cost)
            {
                Global.message = "Your point is not enough for this passing!";
                Global.timer = 3.0f;
                Debug.Log("Your point is not enough for this passing!");
                Global.isPlayerSelected = false;
                Global.isToPass = false;
                Global.player = Global.position = null;
                Global.buttonTimer = 0f;
                light.SetActive(false);
                return;
            }
            Global.buttonTimer = 0f;
            light.SetActive(false);

            Global.desPosition = position;
            Global.player.GetComponent<Player>().playerPosition.GetComponent<Position>().standBall = false;
            position.GetComponent<Position>().standBall = true;
            Global.kickTimer = Global.preKickTime;
            Global.adjustPlayerRotationPass(Global.player, Global.desPosition);
            Global.player.GetComponent<Player>().anime.Play("rig|kick");
            passCountDown = true;        
        }
    }
    
}
