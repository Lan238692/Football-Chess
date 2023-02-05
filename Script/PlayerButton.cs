using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//The class corresponding to the player submenu, which covers the player's shooting, passing and movement actions.

public class PlayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonType;
    public float possibility = 0;

    public GameObject moveButton;
    public GameObject passButton;
    public GameObject shootButton;

    public bool shootCountDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        //Triggers a ball movement event after the player has taken a foot for a period of time.
        if(shootCountDown == true && Global.kickTimer <= 0)
        {
            Global.isReadyToShoot = true;
            Global.movePoint-=3;
            shootCountDown = false;
        }
        
    }

    //Responsible for giving the player some help information when the mouse hangs over the submenu buttons.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Responsible for calculating the shot success rate
        if(buttonType == "Shoot")
        {
            if(Global.position.GetComponent<Position>().standBall == false)
                return;

            Vector3 leftPoint = new Vector3(0f, 0.3f, -11.09f);
            Vector3 rightPoint = new Vector3(0f, 0.3f, 11.09f);
            float distance;
            if(Global.side == Global.half)
                distance = Vector3.Distance(Global.player.transform.localPosition, rightPoint);
            else
                distance = Vector3.Distance(Global.player.transform.localPosition, leftPoint);
            if(distance > 6.5f)
            {
                possibility = 0;
                
            }
            else if(distance > 4.0f)
            {
                possibility = 50;
                for(int i = 0; i < Global.countPlayer(Global.side, Global.half == Global.side); i++)
                {
                    possibility/=2;
                }
            }
            else
            {
                possibility = 100;
                for(int i = 0; i < Global.countPlayer(Global.side, Global.half == Global.side); i++)
                {
                    possibility/=2;
                }
            }

            if(Global.movePoint==2)
                possibility *= 0.6f;
            if(Global.movePoint==1)
                possibility *= 0.2f;

            GameObject.Find("RightUpText").GetComponent<TMP_Text>().text = "GOAL RATE :";
            GameObject.Find("RightUpData").GetComponent<TMP_Text>().text = Mathf.Round(possibility).ToString()+"%";
            Global.shootButtonTimer = 100.0f;
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Global.shootButtonTimer = 0f;
    }

    //Responsible for guiding different actions
    public void OnClick()
    {
		if(buttonType == "Shoot")
        {
            if(Global.isPlayerHoldsBall == false)
            {
                Global.isPlayerSelected = false;
                Global.player = Global.position = null;
                
                Global.message = "This player is not holding the ball!";
                Global.timer = 3.0f;
                Debug.Log("This player is not holding the ball!");
            }
            else
            {
                Debug.Log ("Shoot");
                //shoot do not need confirm the target
                Global.isToShoot = true;
                Global.player.GetComponent<Player>().playerPosition.GetComponent<Position>().standBall = false;

                //decide the ball position during the shoot
                bool leftOrRight = (Global.side == Global.half);
                bool isShootSuccessful = (possibility > Random.Range(0,100));
                if(isShootSuccessful)
                {
                    if(leftOrRight)
                        if(Global.player.transform.localPosition.x>=0)
                            Global.desPos = Global.right2;
                        else
                            Global.desPos = Global.right1;
                    if(!leftOrRight)
                        if(Global.player.transform.localPosition.x>=0)
                            Global.desPos = Global.left2;
                        else
                            Global.desPos = Global.left1;
                    
                    Global.isGoal = true;
                }
                else
                {
                    if(leftOrRight)
                    {
                        Global.desPos = Global.rightGoalball;
                        Global.rightGKPosition.GetComponent<Position>().standBall = true;
                    }
                    if(!leftOrRight)
                    {
                        Global.desPos = Global.leftGoalball;
                        Global.leftGKPosition.GetComponent<Position>().standBall = true;
                    }
                    Global.isGoal = false;
                }
                Global.kickTimer = Global.preKickTime;
                Global.adjustPlayerRotationShoot(Global.player);
                Global.player.GetComponent<Player>().anime.Play("rig|kick");
                shootCountDown = true;
            }

            moveButton.transform.localPosition = Global.hide;
            passButton.transform.localPosition = Global.hide;
            shootButton.transform.localPosition = Global.hide;

        }
        if(buttonType == "Move")
        {
            Debug.Log ("Move");
            Global.isToMove = true;

            moveButton.transform.localPosition = Global.hide;
            passButton.transform.localPosition = Global.hide;
            shootButton.transform.localPosition = Global.hide;
        }
        if(buttonType == "Pass")
        {
            if(Global.isPlayerHoldsBall == false)
            {
                Global.message = "The player is not holding the ball!";
                Global.timer = 3.0f;
                Debug.Log("The player is not holding the ball!");
                Global.isToPass = false;
                Global.isPlayerSelected = false;
                Global.player = Global.position = null;
            }
            else{
                Debug.Log ("Pass");
                Global.isToPass = true;
            }

            moveButton.transform.localPosition = Global.hide;
            passButton.transform.localPosition = Global.hide;
            shootButton.transform.localPosition = Global.hide;
        }
	}
}
