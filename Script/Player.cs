using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//A key class that handles player actions.

public class Player : MonoBehaviour
{
    public GameObject player;
    
    public Canvas mcan;


    public GameObject moveButton;
    public GameObject passButton;
    public GameObject shootButton;

    public GameObject playerPosition;

    public GameObject firstHalfStartPos;
    public GameObject secondHalfStartPos;

    public bool mSide;
    public bool isGK;

    public Animator anime;

    public Camera cam;

    public bool passCountDown = false;

    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Triggers a ball movement event after the player has taken a foot for a period of time.
        if(passCountDown == true && Global.kickTimer <= 0)
        {
            Global.isReadyToPass = true;
            Global.movePoint -= cost;
            passCountDown = false;
        }

        //Resetting of players in the first half (kickoff)
        if(Global.firstHalfKickoff == true)
        {
            if(playerPosition != null)
            {           
                playerPosition.GetComponent<Position>().standBall = false;
                playerPosition.GetComponent<Position>().standPlayer = null;
            }
            playerPosition = firstHalfStartPos;
            player.transform.localPosition = playerPosition.transform.localPosition;
            if(mSide == false)
            {
                player.transform.localEulerAngles  = new Vector3(0f, 40f, 0f);
            }
            else
            {
                player.transform.localEulerAngles  = new Vector3(0f, 140f, 0f);
            }
            playerPosition.GetComponent<Position>().standPlayer = player;
            Global.isPositionChanged++;
        }

        //Resetting of players in the second half (kickoff)
        if(Global.secondHalfKickoff == true)
        {
            if(playerPosition != null)
            {
                playerPosition.GetComponent<Position>().standBall = false;
                playerPosition.GetComponent<Position>().standPlayer = null;
            }
            playerPosition = secondHalfStartPos;
            player.transform.localPosition = playerPosition.transform.localPosition;
            if(mSide == true)
            {
                player.transform.localEulerAngles  = new Vector3(0f, 40f, 0f);
            }
            else
            {
                player.transform.localEulerAngles  = new Vector3(0f, 140f, 0f);
            }
            playerPosition.GetComponent<Position>().standPlayer = player;
            Global.isPositionChanged++;
        }
    }

    //When the mouse is not pointing at a player, there is usually no reaction,
    //except when passing and breakthroughs are made,
    //when an aperture is needed to reflect the target player.
    void OnMouseEnter()
    {
        if(Global.isWorking == true)
            return;
        if(Global.isToMove == false && mSide != Global.side)
            return;
        
        if(Global.player != null)
            if(Global.player == player)
                return;

        if(Global.isToMove == true)
        {
            if(Global.isDistanceOne(Global.player, player) == false)
            {
                return;
            }
            if(player != null)
            {
                if(mSide == Global.player.GetComponent<Player>().mSide)
                    return;
                if(isGK == true)
                    return;
            }

            cost = 1;
            if(Global.position.GetComponent<Position>().standBall == true)
                cost = 2;

            GameObject.Find("RightUpText").GetComponent<TMP_Text>().text = "MOVE COST :";
            GameObject.Find("RightUpData").GetComponent<TMP_Text>().text = cost.ToString();
            Global.buttonTimer = 100.0f;

            playerPosition.GetComponent<Position>().light.transform.localPosition = playerPosition.transform.localPosition;
            playerPosition.GetComponent<Position>().light.SetActive(true);            
        }
        if(Global.isToPass == true)
        {
            if(mSide != Global.player.GetComponent<Player>().mSide)
                return;

            if(Vector3.Distance(Global.position.transform.localPosition, playerPosition.transform.localPosition)<5.5f)
                cost = 1;
            else if(Vector3.Distance(Global.position.transform.localPosition, playerPosition.transform.localPosition)<9.5f)
                cost = 2;
            else
                cost = 3;
            GameObject.Find("RightUpText").GetComponent<TMP_Text>().text = "PASS COST :";
            GameObject.Find("RightUpData").GetComponent<TMP_Text>().text = cost.ToString();
            Global.buttonTimer = 100.0f;

            playerPosition.GetComponent<Position>().light.transform.localPosition = playerPosition.transform.localPosition;
            playerPosition.GetComponent<Position>().light.SetActive(true);     
        }
    }
    void OnMouseExit()
    {
        Global.buttonTimer = 0f;
        playerPosition.GetComponent<Position>().light.SetActive(false);
    }

    //When a player is clicked, either a secondary menu is triggered to invoke a specific action,
    //or he is designated as a passing or breaking target.
    void OnMouseDown()
    {
        if(Global.isWorking == true)
            return;

        if(Global.isToMove == false && mSide != Global.side)
            return;

        if(Global.player != null)
            if(Global.player == player && (Global.isToMove == true||Global.isToPass == true))
                return;

        if(Global.isToMove == true)
        {
            Global.isDribble = false;
            if(player != null)
            {
                if(mSide == Global.player.GetComponent<Player>().mSide)
                {
                    Global.message = "You can't move your player to your teammate!";
                    Global.timer = 3.0f;
                    Debug.Log("You can't move your player to your teammate!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = null;
                    Global.buttonTimer = 0f;
                    playerPosition.GetComponent<Position>().light.SetActive(false);
                    return;
                }
                if(Global.isDistanceOne(Global.player, player) == false)
                {
                    Global.message = "You can't move more than two squares at once!";
                    Global.timer = 3.0f;
                    Debug.Log("You can't move more than two squares at once!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = null;
                    Global.buttonTimer = 0f;
                    playerPosition.GetComponent<Position>().light.SetActive(false);
                    return;
                }
                if(isGK == true)
                {
                    Global.message = "You can't move your player to the goalkeeper!";
                    Global.timer = 3.0f;
                    Debug.Log("You can't move your player to the goalkeeper!");
                    Global.isPlayerSelected = false;
                    Global.isToMove = false;
                    Global.player = null;
                    Global.buttonTimer = 0f;
                    playerPosition.GetComponent<Position>().light.SetActive(false);
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
                    playerPosition.GetComponent<Position>().light.SetActive(false);
                    return;
                }
                Global.isDribble = true;
                Global.dribbledPlayer = player;
            }
            Global.desPosition = playerPosition;
            if(Global.isDribble == false)
            {
                Debug.Log("Error!");
                return;
            }
            else
            {
                Global.player.GetComponent<Player>().playerPosition = playerPosition;
                Global.position.GetComponent<Position>().standPlayer = player;
                playerPosition.GetComponent<Position>().standPlayer = Global.player;
                playerPosition = Global.position;
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
                playerPosition.GetComponent<Position>().standBall = false;
                Global.player.GetComponent<Player>().playerPosition.GetComponent<Position>().standBall = true;
            }
            Global.buttonTimer = 0f;
            Global.isReadyToMove = true;
            Global.adjustPlayerRotationMove(Global.player, Global.desPosition, Global.isPlayerHoldsBall);
            Global.player.GetComponent<Player>().anime.Play("rig|walk");
            Global.player.GetComponent<Player>().playerPosition.GetComponent<Position>().light.SetActive(false);
            return;
        }
        if(Global.isToPass == true)
        {
            if(mSide!=Global.player.GetComponent<Player>().mSide)
            {
                Global.message = "You can't pass the ball to the opposite side!";
                Global.timer = 3.0f;
                Debug.Log("You can't pass the ball to the opposite side!");
                Global.isPlayerSelected = false;
                Global.isToPass = false;
                Global.player = null;
                Global.buttonTimer = 0f;
                playerPosition.GetComponent<Position>().light.SetActive(false);
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
                playerPosition.GetComponent<Position>().light.SetActive(false);
                return;
            }
            Global.buttonTimer = 0f;
            playerPosition.GetComponent<Position>().light.SetActive(false);

            Global.desPosition = playerPosition;
            Global.position.GetComponent<Position>().standBall = false;
            playerPosition.GetComponent<Position>().standBall = true;
            Global.kickTimer = Global.preKickTime;
            Global.adjustPlayerRotationPass(Global.player, Global.desPosition);
            Global.player.GetComponent<Player>().anime.Play("rig|kick");
            passCountDown = true;

            return;
        }           

        Global.isToMove = false;
        Global.isToPass = false;
        Global.isToShoot = false;

        //open menu at player's position
        var canvasRT = mcan.GetComponent<RectTransform>();
        var canvasCam = mcan.worldCamera;
        
        Vector2 UIPos;
        Vector3 screenPos = cam.WorldToScreenPoint(player.transform.localPosition);        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT,screenPos, canvasCam,out UIPos);

        if(isGK == true)
        {
            Vector2 passButtonPos = UIPos - new Vector2(0f, 80f);
            passButton.transform.localPosition = passButtonPos;
            moveButton.transform.localPosition = Global.hide; 
            shootButton.transform.localPosition = Global.hide; 
        }
        else
        {
            Vector2 moveButtonPos = UIPos - new Vector2(120f, 45f);
            Vector2 shootButtonPos = UIPos - new Vector2(0f, 90f);
            Vector2 passButtonPos = UIPos - new Vector2(-120f, 45f);
            moveButton.transform.localPosition = moveButtonPos; 
            shootButton.transform.localPosition = shootButtonPos;
            passButton.transform.localPosition = passButtonPos; 
        }
       
        Global.isPlayerSelected = true;
        Global.position = playerPosition;
        Global.player = player;
        if(playerPosition.GetComponent<Position>().standBall == true)
        {
            Global.isPlayerHoldsBall = true;
        }
        else
        {
            Global.isPlayerHoldsBall = false;
        }
    }
}
