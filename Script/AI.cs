using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI : MonoBehaviour
{
    public int[,] pos;
    public int[,] costMap;
    public int[,] costMapB;
    public int[] ballPos;
    public int[,] allyPos;
    public int[,] enemyPos;

    public int ally = 9999;
    public int enemy = 1;
    public int empty = 0;
    //this means the move cost(no ball) of different grids, moving to an ally is impossible so the cost will be infinite
    public int allyM = 9999;
    public int enemyM = 1;
    public int emptyM = 1;
    //this means the move cost(have ball) of different grids, moving to an ally is impossible so the cost will be infinite
    public int allyMB = 9999;
    public int enemyMB = 2;
    public int emptyMB = 1;

    public int GK = 10;

    public int[] moveDes;
    public int[] playerDes;

    public bool passCountDown = false;
    public bool shootCountDown = false;
    public int finalCost = -1;

    // Start is called before the first frame update
    void Start()
    {
        pos = new int[12, 9];
        costMap = new int[12, 9];
        costMapB = new int[12, 9];
        ballPos = new int[2];
        allyPos = new int[11, 2];
        enemyPos = new int[11, 2];
        moveDes = new int[2];
        playerDes = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.isHavingAI == false)
            return;

        if(shootCountDown == true && Global.kickTimer <= 0)
        {
            Global.isReadyToShoot = true;
            Global.movePoint -= 3;
            shootCountDown = false;
        }
        if (passCountDown == true && Global.kickTimer <= 0)
        {
            Global.isReadyToPass = true;
            Global.movePoint -= finalCost;
            passCountDown = false;
        }

        if(shootCountDown == true || passCountDown == true)
            return;

        if(Global.side == true && Global.isWorking == false && Global.movePoint > 0)
        {
            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 9; j++)
                {
                    pos[i, j] = empty;
                    costMap[i, j] = emptyM;
                    costMapB[i, j] = emptyMB;
                }
            for(int i = 0; i < 11; i++)
            {
                string strColumn;
                string strRow;
                int column;
                int row;
                string posName;

                posName = Global.homeTeamPlayer[i].GetComponent<Player>().playerPosition.name;
                if(posName.Length == 6)
                {
                    strColumn = posName.Substring(3, 1);
                    strRow = posName.Substring(5, 1);
                }
                else
                {
                    strColumn = posName.Substring(3, 2);
                    strRow = posName.Substring(6, 1);
                }
                column = Convert.ToInt32(strColumn);
                row = Convert.ToInt32(strRow);
                pos[column-1, row-1] = enemy;
                costMap[column-1, row-1] = enemyM;
                costMapB[column-1, row-1] = enemyMB;
                enemyPos[i, 0] = column - 1;
                enemyPos[i, 1] = row -1;
                if(Global.homeTeamPlayer[i].GetComponent<Player>().playerPosition.GetComponent<Position>().standBall == true)
                {
                    ballPos[0] = column - 1;
                    ballPos[1] = row - 1;
                }

                posName = Global.awayTeamPlayer[i].GetComponent<Player>().playerPosition.name;
                if(posName.Length == 6)
                {
                    strColumn = posName.Substring(3, 1);
                    strRow = posName.Substring(5, 1);
                }
                else
                {
                    strColumn = posName.Substring(3, 2);
                    strRow = posName.Substring(6, 1);
                }
                column = Convert.ToInt32(strColumn);
                row = Convert.ToInt32(strRow);
                pos[column-1, row-1] = ally;
                costMap[column-1, row-1] = allyM;
                costMapB[column-1, row-1] = allyMB;
                allyPos[i, 0] = column -1;
                allyPos[i, 1] = row -1;
                if(Global.awayTeamPlayer[i].GetComponent<Player>().playerPosition.GetComponent<Position>().standBall == true)
                {
                    ballPos[0] = column - 1;
                    ballPos[1] = row - 1;
                }
            }
            if(pos[ballPos[0], ballPos[1]] == enemy)
            {
                int closestPlayer = -1;
                int closestDistance = 999;
                for(int i = 0; i < 10; i++)
                {
                    int[] temp = new int[2];
                    temp[0] = allyPos[i,0];
                    temp[1] = allyPos[i,1];
                    int tempDis = playerDistance(temp, ballPos, false);
                    //Debug.Log(tempDis);
                    if(tempDis < closestDistance)
                    {
                        closestDistance = tempDis;
                        closestPlayer = i;
                    }
                    else if(tempDis == closestDistance)
                    {
                        if(throwCoin())
                            closestPlayer = i;
                    }
                }
                if(closestDistance == 999)
                    Debug.Log("no path found!");
                
                string sourcePos = string.Format("Pos{0}-{1}", allyPos[closestPlayer, 0] + 1, allyPos[closestPlayer, 1] + 1);
                string targetPos = "";
                if(allyPos[closestPlayer, 0] == ballPos[0])
                {
                    if(allyPos[closestPlayer, 1] < ballPos[1])
                    {
                        targetPos = string.Format("Pos{0}-{1}", allyPos[closestPlayer, 0] + 1, allyPos[closestPlayer, 1] + 2);
                    }
                    if(allyPos[closestPlayer, 1] > ballPos[1])
                    {
                        targetPos = string.Format("Pos{0}-{1}", allyPos[closestPlayer, 0] + 1, allyPos[closestPlayer, 1] + 0);
                    }
                }
                if(allyPos[closestPlayer, 1] == ballPos[1])
                {
                    if(allyPos[closestPlayer, 0] < ballPos[0])
                    {
                        targetPos = string.Format("Pos{0}-{1}", allyPos[closestPlayer, 0] + 2, allyPos[closestPlayer, 1] + 1);
                    }
                    if(allyPos[closestPlayer, 0] > ballPos[0])
                    {
                        targetPos = string.Format("Pos{0}-{1}", allyPos[closestPlayer, 0] + 0, allyPos[closestPlayer, 1] + 1);
                    }
                }

                int[] temp1 = new int[2];
                int[] temp2 = new int[2];
                int[] temp3 = new int[2];
                if(allyPos[closestPlayer, 0] < ballPos[0])
                {
                    temp1[0] = allyPos[closestPlayer, 0];
                    temp2[0] = allyPos[closestPlayer, 0] + 1;
                    temp3[0] = allyPos[closestPlayer, 0] + 1;
                }
                else
                {
                    temp1[0] = allyPos[closestPlayer, 0];
                    temp2[0] = allyPos[closestPlayer, 0] - 1;
                    temp3[0] = allyPos[closestPlayer, 0] - 1;
                }
                if(allyPos[closestPlayer, 1] < ballPos[1])
                {
                    temp1[1] = allyPos[closestPlayer, 1] + 1;
                    temp2[1] = allyPos[closestPlayer, 1] + 1;
                    temp3[1] = allyPos[closestPlayer, 1];
                }
                else
                {
                    temp1[1] = allyPos[closestPlayer, 1] - 1;
                    temp2[1] = allyPos[closestPlayer, 1] - 1;
                    temp3[1] = allyPos[closestPlayer, 1];
                }
                int cost1 = playerDistance(temp1, ballPos, false);
                int cost2 = playerDistance(temp2, ballPos, false);
                int cost3 = playerDistance(temp3, ballPos, false);
                if(costMap[temp2[0], temp2[1]] + cost2 == closestDistance)
                {
                    targetPos = string.Format("Pos{0}-{1}", temp2[0] + 1, temp2[1] + 1);
                }
                if(costMap[temp1[0], temp1[1]] + cost1 == closestDistance)
                {
                    targetPos = string.Format("Pos{0}-{1}", temp1[0] + 1, temp1[1] + 1);
                }
                if(costMap[temp3[0], temp3[1]] + cost3 == closestDistance)
                {
                    targetPos = string.Format("Pos{0}-{1}", temp3[0] + 1, temp3[1] + 1);
                }
                executeMove(sourcePos, targetPos, false);                
                //Debug.Log(sourcePos);
                //Debug.Log(targetPos);
                return;
            }
            if(pos[ballPos[0], ballPos[1]] == ally)
            {
                int player = -1;
                //reset
                moveDes[0] = moveDes[1] = -1;
                playerDes[0] = playerDes[1] = -1;
                for(int i = 0; i < 11; i++)
                {
                    if(allyPos[i, 0] == ballPos[0] && allyPos[i, 1] == ballPos[1])
                    {
                        player = i;
                    }
                }
                if(player == -1)
                {
                    Debug.Log("ERROR: can't find the player with ball!");
                    Global.leftPoint -= 1;
                    return;
                }

                //firstly, if the ball is at GK's position, AI will firstly pass out
                //Here is a special case, if all players run upfield, 
                //the goalkeeper will face a situation where there is no one to pass to.
                //But, it is almost impossible for AI
                if(player == GK)
                {
                    if(Global.movePoint < 3)
                        Debug.Log("ERROR: GK with move point less than 3!");
                    int targetPlayer = -1;
                    //float distance = -1;
                    int targetZ;
                    if(Global.side == Global.half)
                        targetZ = -1;
                    else
                        targetZ = 99;
                    string kickPos;
                    string savePos = "";
                    if(Global.side == Global.half)
                        kickPos = string.Format("Pos1-5");
                    else
                        kickPos = string.Format("Pos12-5");
                    GameObject kickPosition = GameObject.Find(kickPos);
                    for(int i = 0; i < 10; i++)
                    {
                        string tempPos = string.Format("Pos{0}-{1}", allyPos[i, 0] + 1, allyPos[i, 1] + 1);
                        GameObject targetPosition = GameObject.Find(tempPos);
                        if(allyPos[i, 0] < targetZ && (Global.side != Global.half))
                        {
                            if(Vector3.Distance(kickPosition.transform.localPosition, targetPosition.transform.localPosition) < 13.5)
                            {
                                targetZ = allyPos[i, 0];
                                targetPlayer = i;
                                savePos = tempPos;
                            }
                        }
                        if(allyPos[i, 0] > targetZ && (Global.side == Global.half))
                        {
                            if(Vector3.Distance(kickPosition.transform.localPosition, targetPosition.transform.localPosition) < 13.5)
                            {
                                targetZ = allyPos[i, 0];
                                targetPlayer = i;
                                savePos = tempPos;
                            }
                        }
                    }
                    if(targetPlayer == -1)
                    {
                        Debug.Log("ERROR: GK doesn't have a pass target!");
                        Global.leftPoint -= 1;
                        return;
                    }
                    else
                    {
                        executePass(kickPos, savePos);
                        return;
                    }
                }

                //randomMove();
                //return;

                string sourcePos = string.Format("Pos{0}-{1}", allyPos[player, 0] + 1, allyPos[player, 1] + 1);
                string targetPos = "";
                //then, AI will judge its movement by move point
                if(Global.leftPoint >= 6)
                {
                    int passCost = findPassTarget(player, 3);
                    if(passCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", playerDes[0] + 1, playerDes[1] + 1);
                        executePass(sourcePos, targetPos);
                        return;
                    }
                    int moveCost = findMoveTarget(player, 2);
                    if(moveCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", moveDes[0] + 1, moveDes[1] + 1);
                        executeMove(sourcePos, targetPos, true);
                        return;
                    }
                    randomMove();
                    return;
                }
                else if(Global.leftPoint == 5)
                {
                    int passCost = findPassTarget(player, 2);
                    if(passCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", playerDes[0] + 1, playerDes[1] + 1);
                        executePass(sourcePos, targetPos);
                        return;
                    }
                    int moveCost = findMoveTarget(player, 2);
                    if(moveCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", moveDes[0] + 1, moveDes[1] + 1);
                        executeMove(sourcePos, targetPos, true);
                        return;
                    }
                    randomMove();
                    return;
                }
                else if(Global.leftPoint == 4)
                {
                    int passCost = findPassTarget(player, 1);
                    if(passCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", playerDes[0] + 1, playerDes[1] + 1);
                        executePass(sourcePos, targetPos);
                        return;
                    }
                    int moveCost = findMoveTarget(player, 1);
                    if(moveCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", moveDes[0] + 1, moveDes[1] + 1);
                        executeMove(sourcePos, targetPos, true);
                        return;
                    }
                    randomMove();
                    return;
                }
                else if(Global.leftPoint == 3)
                {
                    float mPossibility = possibilityForShooting(player);
                    if(mPossibility >= 30f)
                    {
                        executeShoot(sourcePos, mPossibility);
                        return;
                    }
                    int passCost = findPassTarget(player, 3);
                    if(passCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", playerDes[0] + 1, playerDes[1] + 1);
                        executePass(sourcePos, targetPos);
                        return;
                    }
                    int moveCost = findMoveTarget(player, 2);
                    if(moveCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", moveDes[0] + 1, moveDes[1] + 1);
                        executeMove(sourcePos, targetPos, true);
                        return;
                    }
                    randomMove();
                    return;
                }
                else if(Global.leftPoint == 2)
                {
                    int passCost = findPassTarget(player, 2);
                    if(passCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", playerDes[0] + 1, playerDes[1] + 1);
                        executePass(sourcePos, targetPos);
                        return;
                    }
                    int moveCost = findMoveTarget(player, 2);
                    if(moveCost != -1)
                    {
                        targetPos = string.Format("Pos{0}-{1}", moveDes[0] + 1, moveDes[1] + 1);
                        executeMove(sourcePos, targetPos, true);
                        return;
                    }
                    randomMove();
                    return;
                }
                else
                {
                    randomMove();
                    return;
                }

            }
        }
    }
    int findPassTarget(int playerNo, int limit = 3)
    {
        playerDes[0] = -1;
        playerDes[1] = -1;
        float passCost = -1;

        int[] GKPos = new int[2];
        if(Global.half == Global.side)
        {
            GKPos[0] = 11;
            GKPos[1] = 4; 
        }
        else
        {
            GKPos[0] = 0;
            GKPos[1] = 4;
        }

        string srcPos = string.Format("Pos{0}-{1}", allyPos[playerNo, 0] + 1, allyPos[playerNo, 1] + 1);
        GameObject srcPosition = GameObject.Find(srcPos);
        int[] oriPos = new int[2];
        oriPos[0] = allyPos[playerNo, 0];
        oriPos[1] = allyPos[playerNo, 1];
        int[] bestPos = new int[2];
        float bestRatio = -1f;

        int originCost = playerDistance(oriPos, GKPos, true);
        for(int i = 0; i < 10; i++)
        {
            if(i == playerNo)
                continue;
            
            int[] newPos = new int[2];
            newPos[0] = allyPos[i, 0];
            newPos[1] = allyPos[i, 1];
            if(computeDistance(oriPos, newPos)>=13.5f)
                continue;
            if(computeDistance(oriPos, newPos)>=9.5f)
                passCost = 3f;
            else if(computeDistance(oriPos, newPos)>=5.5f)
                passCost = 2f;
            else
                passCost = 1f;
            if(passCost>limit)
                continue;

            int newCost = playerDistance(newPos, GKPos, true);
            if(newCost >= originCost)
                continue;
            
            float ratio = ((float)originCost-(float)newCost)/passCost;
            if(ratio>bestRatio)
            {
                bestRatio = ratio;
                bestPos[0] = newPos[0];
                bestPos[1] = newPos[1];
            }
        }
        if(bestRatio == -1f)
        {
            Debug.Log("Can't find proper passing position!");
            return -1;
        }
        playerDes[0] = bestPos[0];
        playerDes[1] = bestPos[1];
        return (int)passCost;
    }
    float possibilityForShooting(int playerNo)
    {
        float possibility = 0f;
        string shootPos = string.Format("Pos{0}-{1}", allyPos[playerNo, 0] + 1, allyPos[playerNo, 1] + 1);
        GameObject shootPosition = GameObject.Find(shootPos);
        Global.player = shootPosition.GetComponent<Position>().standPlayer;
        string GKPos = "";
        if(Global.half == Global.side)
            GKPos = "Pos12-5";
        else
            GKPos = "Pos1-5";
        GameObject GKPosition = GameObject.Find(GKPos);
        float distance = Vector3.Distance(shootPosition.transform.localPosition, GKPosition.transform.localPosition);
        if(distance > 6.5f)
        {
            possibility = 0f;
        }
        else if(distance > 4.0f)
        {
            possibility = 40f;
            for(int i = 0; i < Global.countPlayer(Global.side, Global.half == Global.side); i++)
            {
                possibility/=2;
            }
        }
        else
        {
            possibility = 90f;
            for(int i = 0; i < Global.countPlayer(Global.side, Global.half == Global.side); i++)
            {
                possibility/=2;
            }
        }
        return possibility;
    }
    int findMoveTarget(int playerNo, int limit = 2)
    {
        moveDes[0] = -1;
        moveDes[1] = -1;
        
        int[] GKPos = new int[2];
        if(Global.half == Global.side)
        {
            GKPos[0] = 11;
            GKPos[1] = 4; 
        }
        else
        {
            GKPos[0] = 0;
            GKPos[1] = 4;
        }

        if((allyPos[playerNo, 0]-GKPos[0])*(allyPos[playerNo, 0]-GKPos[0])+(allyPos[playerNo, 1]-GKPos[1])*(allyPos[playerNo, 1]-GKPos[1])<=2)
        {
            Debug.Log("Too close(not a bug)");
            return -1;
        }

        string srcPos = string.Format("Pos{0}-{1}", allyPos[playerNo, 0] + 1, allyPos[playerNo, 1] + 1);
        GameObject srcPosition = GameObject.Find(srcPos);

        if(allyPos[playerNo, 0] == GKPos[0])
        {
            int[] tempPos = new int[2];
            if(allyPos[playerNo, 1] < GKPos[1])
            {
                tempPos[0] = allyPos[playerNo, 0];
                tempPos[1] = allyPos[playerNo, 1] + 1; 
            }
            else
            {
                tempPos[0] = allyPos[playerNo, 0];
                tempPos[1] = allyPos[playerNo, 1] - 1;
            }
            if(costMapB[tempPos[0], tempPos[1]] == allyMB)
            {
                return -1;
            }
            if(costMapB[tempPos[0], tempPos[1]] == enemyMB)
            {
                if(limit < enemyMB)
                {
                    return -1;
                }
                moveDes[0] = tempPos[0];
                moveDes[1] = tempPos[1];
                return enemyMB;
            }
            if(costMapB[tempPos[0], tempPos[1]] == emptyMB)
            {
                moveDes[0] = tempPos[0];
                moveDes[1] = tempPos[1];
                return emptyMB;
            }
            Debug.Log("ERROR: unexpected cost!");
            return -1;
        }
        if(allyPos[playerNo, 1] == GKPos[1])
        {
            int[] tempPos = new int[2];
            if(allyPos[playerNo, 0] < GKPos[0])
            {
                tempPos[1] = allyPos[playerNo, 1];
                tempPos[0] = allyPos[playerNo, 0] + 1; 
            }
            else
            {
                tempPos[1] = allyPos[playerNo, 1];
                tempPos[0] = allyPos[playerNo, 0] - 1;
            }
            if(costMapB[tempPos[0], tempPos[1]] == allyMB)
            {
                return -1;
            }
            if(costMapB[tempPos[0], tempPos[1]] == enemyMB)
            {
                if(limit < enemyMB)
                {
                    return -1;
                }
                moveDes[0] = tempPos[0];
                moveDes[1] = tempPos[1];
                return enemyMB;
            }
            if(costMapB[tempPos[0], tempPos[1]] == emptyMB)
            {
                moveDes[0] = tempPos[0];
                moveDes[1] = tempPos[1];
                return emptyMB;
            }
            Debug.Log("ERROR: unexpected cost!");
            return -1;
        }

        int[] temp1 = new int[2];
        int[] temp2 = new int[2];
        int[] temp3 = new int[2];
        if(allyPos[playerNo, 0] < GKPos[0])
        {
            temp1[0] = allyPos[playerNo, 0];
            temp2[0] = allyPos[playerNo, 0] + 1;
            temp3[0] = allyPos[playerNo, 0] + 1;
        }
        else
        {
            temp1[0] = allyPos[playerNo, 0];
            temp2[0] = allyPos[playerNo, 0] - 1;
            temp3[0] = allyPos[playerNo, 0] - 1;
        }
        if(allyPos[playerNo, 1] < GKPos[1])
        {
            temp1[1] = allyPos[playerNo, 1] + 1;
            temp2[1] = allyPos[playerNo, 1] + 1;
            temp3[1] = allyPos[playerNo, 1];
        }
        else
        {
            temp1[1] = allyPos[playerNo, 1] - 1;
            temp2[1] = allyPos[playerNo, 1] - 1;
            temp3[1] = allyPos[playerNo, 1];
        }
        int cost1 = playerDistance(temp1, GKPos, true);
        int cost2 = playerDistance(temp2, GKPos, true);
        int cost3 = playerDistance(temp3, GKPos, true);
        if(costMapB[temp1[0], temp1[1]] == allyMB || (costMapB[temp1[0], temp1[1]] == enemyMB && limit < enemyMB))
        {
            cost1 = 9999;
        }
        if(costMapB[temp2[0], temp2[1]] == allyMB || (costMapB[temp2[0], temp2[1]] == enemyMB && limit < enemyMB))
        {
            cost2 = 9999;
        }
        if(costMapB[temp3[0], temp3[1]] == allyMB || (costMapB[temp3[0], temp3[1]] == enemyMB && limit < enemyMB))
        {
            cost3 = 9999;
        }
        int minValue = min(cost1, cost2, cost3);
        if(minValue >= 9999)
        {
            return -1;
        }
        //It is not the most accurate to use cost directly here, 
        //the best practice is to add the cost when moving the first step, but this is not too big a problem.
        if(minValue == cost2)
        {
            moveDes[0] = temp2[0];
            moveDes[1] = temp2[1];
            return costMapB[temp3[0], temp2[1]];
        }
        if(minValue == cost3)
        {
            moveDes[0] = temp3[0];
            moveDes[1] = temp3[1];
            return costMapB[temp3[0], temp3[1]];
        }
        if(minValue == cost1)
        {
            moveDes[0] = temp1[0];
            moveDes[1] = temp1[1];
            return costMapB[temp1[0], temp1[1]];
        }

        Debug.Log("ERROR: unexpected minValue!");
        return -1;

    }
    void randomMove()
    {
        int shortestDistance = 99999;
        int selectedPlayer = -1;
        int[] GKPos = new int[2];
        if(Global.half == Global.side)
        {
            GKPos[0] = 11;
            GKPos[1] = 4; 
        }
        else
        {
            GKPos[0] = 0;
            GKPos[1] = 4;
        }

        for(int i = 0; i < 10; i++)
        {
            //Debug.Log("Pos"+allyPos[i, 0]+"-"+allyPos[i,1]);
            if(ballPos[0] == allyPos[i, 0] && ballPos[1] == allyPos[i, 1])
                continue;
            int[] tempPos = new int[2];
            tempPos[0] = allyPos[i, 0];
            tempPos[1] = allyPos[i, 1];
            if(playerDistance(tempPos, GKPos, false) >= 9999)
                continue;
            int tempDis = (allyPos[i, 0]-GKPos[0])*(allyPos[i, 0]-GKPos[0])+(allyPos[i, 1]-GKPos[1])*(allyPos[i, 1]-GKPos[1]);
            if(tempDis <= 2)
                continue;
            if(tempDis < shortestDistance)
            {
                shortestDistance = tempDis;
                selectedPlayer = i;
            }
            if(tempDis == shortestDistance)
            {
                if(throwCoin())
                    selectedPlayer = i;
            }
        }
        Debug.Log(selectedPlayer);
        if(selectedPlayer == -1)
        {
            Debug.Log("can't find player for random moving!");
            Global.leftPoint -= 1;
            return;
        }

        string sourcePos = string.Format("Pos{0}-{1}", allyPos[selectedPlayer, 0] + 1, allyPos[selectedPlayer, 1] + 1);
        string targetPos = "";

        int[] temp1 = new int[2];
        int[] temp2 = new int[2];
        int[] temp3 = new int[2];
        int resultColumn = -1;
        int resultRow = -1;

        if(GKPos[0] == allyPos[selectedPlayer, 0])
        {
            if(GKPos[1] < allyPos[selectedPlayer, 1])
            {
                resultColumn = allyPos[selectedPlayer, 0];
                resultRow = allyPos[selectedPlayer, 1] - 1;
                targetPos = string.Format("Pos{0}-{1}", resultColumn + 1, resultRow + 1);
                executeMove(sourcePos, targetPos, false);
                return;
            }
            if(GKPos[1] > allyPos[selectedPlayer, 1])
            {
                resultColumn = allyPos[selectedPlayer, 0];
                resultRow = allyPos[selectedPlayer, 1] + 1;
                targetPos = string.Format("Pos{0}-{1}", resultColumn + 1, resultRow + 1);
                executeMove(sourcePos, targetPos, false);
                return;
            }
            Debug.Log("Unexpected position");
        }
        if(GKPos[1] == allyPos[selectedPlayer, 1])
        {
            if(GKPos[0] < allyPos[selectedPlayer, 0])
            {
                resultColumn = allyPos[selectedPlayer, 0] - 1;
                resultRow = allyPos[selectedPlayer, 1];
                targetPos = string.Format("Pos{0}-{1}", resultColumn + 1, resultRow + 1);
                executeMove(sourcePos, targetPos, false);
                return;
            }
            if(GKPos[0] > allyPos[selectedPlayer, 0])
            {
                resultColumn = allyPos[selectedPlayer, 0] + 1;
                resultRow = allyPos[selectedPlayer, 1];
                targetPos = string.Format("Pos{0}-{1}", resultColumn + 1, resultRow + 1);
                executeMove(sourcePos, targetPos, false);
                return;
            }
            Debug.Log("Unexpected position");
        }
        if(allyPos[selectedPlayer, 0] < GKPos[0])
        {
            temp1[0] = allyPos[selectedPlayer, 0];
            temp2[0] = allyPos[selectedPlayer, 0] + 1;
            temp3[0] = allyPos[selectedPlayer, 0] + 1;
        }
        else
        {
            temp1[0] = allyPos[selectedPlayer, 0];
            temp2[0] = allyPos[selectedPlayer, 0] - 1;
            temp3[0] = allyPos[selectedPlayer, 0] - 1;
        }
        if(allyPos[selectedPlayer, 1] < GKPos[1])
        {
            temp1[1] = allyPos[selectedPlayer, 1] + 1;
            temp2[1] = allyPos[selectedPlayer, 1] + 1;
            temp3[1] = allyPos[selectedPlayer, 1];
        }
        else
        {
            temp1[1] = allyPos[selectedPlayer, 1] - 1;
            temp2[1] = allyPos[selectedPlayer, 1] - 1;
            temp3[1] = allyPos[selectedPlayer, 1];
        }
        int cost1 = playerDistance(temp1, GKPos, false);
        int cost2 = playerDistance(temp2, GKPos, false);
        int cost3 = playerDistance(temp3, GKPos, false);
        int minValue = min(cost1 + costMap[temp1[0], temp1[1]], cost2 + costMap[temp2[0], temp2[1]], cost3 + costMap[temp3[0], temp3[1]]);
        if(minValue == cost2 + 1 && costMap[temp2[0], temp2[1]] == 1)
        {
            targetPos = string.Format("Pos{0}-{1}", temp2[0] + 1, temp2[1] + 1);
            executeMove(sourcePos, targetPos, false);
            return;
        }
        if(minValue == cost1 + 1 && costMap[temp1[0], temp1[1]] == 1)
        {
            targetPos = string.Format("Pos{0}-{1}", temp1[0] + 1, temp1[1] + 1);
            executeMove(sourcePos, targetPos, false);
            return;
        }
        if(minValue == cost3 + 1 && costMap[temp3[0], temp3[1]] == 1)
        {
            targetPos = string.Format("Pos{0}-{1}", temp3[0] + 1, temp3[1] + 1);
            executeMove(sourcePos, targetPos, false);
            return;
        }
    }

    int min(int a, int b, int c)
    {
        if(a <= b && a <= c)
            return a;
        if(b <= a && b <= c)
            return b;
        if(c <= a && c <= b)
            return c;
        Debug.Log("bug!");
        return -1;
    }

    //Calculate the shortest distance from posA to posB using dynamic programming
    int playerDistance(int[] posA, int[] posB, bool isHavingBall)
    {
        if(posA[0] == posB[0] && posA[1] == posB[1])
            return 0;
        
        if(posA[0] == posB[0])
        {
            int[] temp = new int[2];
            temp[0] = posA[0];
            if(posA[1] < posB[1])
                temp[1] = posA[1] + 1;
            else
                temp[1] = posA[1] - 1;
            if(isHavingBall == false)
                return costMap[temp[0], temp[1]] + playerDistance(temp, posB, false);
            else
                return costMapB[temp[0], temp[1]] + playerDistance(temp, posB, true);
        }

        if(posA[1] == posB[1])
        {
            int[] temp = new int[2];
            temp[1] = posA[1];
            if(posA[0] < posB[0])
                temp[0] = posA[0] + 1;
            else
                temp[0] = posA[0] - 1;
            if(isHavingBall == false)
                return costMap[temp[0], temp[1]] + playerDistance(temp, posB, false);
            else
                return costMapB[temp[0], temp[1]] + playerDistance(temp, posB, true);
        }

        int[] temp1 = new int[2];
        int[] temp2 = new int[2];
        int[] temp3 = new int[2];
        if(posA[0] < posB[0])
        {
            temp1[0] = posA[0];
            temp2[0] = posA[0] + 1;
            temp3[0] = posA[0] + 1;
        }
        else
        {
            temp1[0] = posA[0];
            temp2[0] = posA[0] - 1;
            temp3[0] = posA[0] - 1;
        }
        if(posA[1] < posB[1])
        {
            temp1[1] = posA[1] + 1;
            temp2[1] = posA[1] + 1;
            temp3[1] = posA[1];
        }
        else
        {
            temp1[1] = posA[1] - 1;
            temp2[1] = posA[1] - 1;
            temp3[1] = posA[1];
        }
        if(isHavingBall == false)
            return min(costMap[temp1[0], temp1[1]]+playerDistance(temp1, posB, false), 
                    costMap[temp2[0], temp2[1]]+playerDistance(temp2, posB, false), 
                    costMap[temp3[0], temp3[1]]+playerDistance(temp3, posB, false));
        else
            return min(costMapB[temp1[0], temp1[1]]+playerDistance(temp1, posB, true), 
                    costMapB[temp2[0], temp2[1]]+playerDistance(temp2, posB, true), 
                    costMapB[temp3[0], temp3[1]]+playerDistance(temp3, posB, true));
    }

    void executeShoot(string sourcePos, float shootPossibility)
    {
        GameObject mSrcPosition = GameObject.Find(sourcePos);
        Global.player = mSrcPosition.GetComponent<Position>().standPlayer;
        mSrcPosition.GetComponent<Position>().standBall = false;
        bool isShootSuccessful = (shootPossibility > UnityEngine.Random.Range(0,100));
        if(isShootSuccessful)
        {
            if(Global.side == Global.half)
            {
                if(Global.player.transform.localPosition.x>=0)
                    Global.desPos = Global.right2;
                else
                    Global.desPos = Global.right1;
            }
            else
            {
                if(Global.player.transform.localPosition.x>=0)
                    Global.desPos = Global.left2;
                else
                    Global.desPos = Global.left1;
            }
            Global.isGoal = true;
        }
        else
        {
            if(Global.side == Global.half)
            {
                Global.desPos = Global.rightGoalball;
                Global.rightGKPosition.GetComponent<Position>().standBall = true;
            }
            if(!Global.side == Global.half)
            {
                Global.desPos = Global.leftGoalball;
                Global.leftGKPosition.GetComponent<Position>().standBall = true;
            }
            Global.isGoal = false;
        }
        /*
        Global.isReadyToShoot = true;
        Global.movePoint -= 3;
        */
        Global.kickTimer = Global.preKickTime;
        Global.adjustPlayerRotationShoot(Global.player);
        Global.player.GetComponent<Player>().anime.Play("rig|kick");
        shootCountDown = true;
    }
    
    void executePass(string sourcePos, string targetPos)
    {
        GameObject mSrcPosition = GameObject.Find(sourcePos);
        GameObject mDesPosition = GameObject.Find(targetPos);
        Global.player = mSrcPosition.GetComponent<Position>().standPlayer;
        Global.desPosition = mDesPosition;

        Global.isWorking = true;
        int cost = -1;
        if (Vector3.Distance(mSrcPosition.transform.localPosition, mDesPosition.transform.localPosition) < 5.5f)
            cost = 1;
        else if (Vector3.Distance(mSrcPosition.transform.localPosition, mDesPosition.transform.localPosition) < 9.5f)
            cost = 2;
        else if (Vector3.Distance(mSrcPosition.transform.localPosition, mDesPosition.transform.localPosition) < 13.5f)
            cost = 3;
        else
        {
            Debug.Log("ERROR: Pass condition not good!(Too long)");
        }
        
        if(Global.leftPoint < cost)
        {
            Debug.Log("ERROR: Pass condition not good!(No enough movepoint)");
        }

        mSrcPosition.GetComponent<Position>().standBall = false;
        mDesPosition.GetComponent<Position>().standBall = true;

        //Global.isReadyToPass = true;
        //Global.movePoint -= cost;
        finalCost = cost;

        Global.kickTimer = Global.preKickTime;
        Global.adjustPlayerRotationPass(Global.player, Global.desPosition);
        Global.player.GetComponent<Player>().anime.Play("rig|kick");
        passCountDown = true;

    }


    void executeMove(string sourcePos, string targetPos, bool isHavingBall)
    {
        GameObject mSrcPosition = GameObject.Find(sourcePos);
        GameObject mDesPosition = GameObject.Find(targetPos);
        Global.player = mSrcPosition.GetComponent<Position>().standPlayer;
        Global.player.GetComponent<Player>().playerPosition = mDesPosition;
        Global.srcPos = mSrcPosition.transform.localPosition;
        Global.desPosition = mDesPosition;
        Global.isWorking = true;
        Global.isPlayerHoldsBall = isHavingBall;

        if(mDesPosition.GetComponent<Position>().standPlayer != null)
        {
            Global.isDribble = true;
            Global.dribbledPlayer = mDesPosition.GetComponent<Position>().standPlayer;
            Global.dribbledPlayer.GetComponent<Player>().playerPosition = mSrcPosition;
            mSrcPosition.GetComponent<Position>().standPlayer = Global.dribbledPlayer;
            if(Global.dribbledPlayer.GetComponent<Player>().isGK == true)
                Global.isDribbleGK = true;
            else
                Global.isDribbleGK = false;
            Global.adjustPlayerRotationMove(Global.dribbledPlayer, Global.player, false);
            Global.dribbledPlayer.GetComponent<Player>().anime.Play("rig|walk");
        }
        else
        {
            Global.isDribble = false;
            mSrcPosition.GetComponent<Position>().standPlayer = null;
        }
        if(isHavingBall)
        {
            mSrcPosition.GetComponent<Position>().standBall = false;
            mDesPosition.GetComponent<Position>().standBall = true;
        }
        mDesPosition.GetComponent<Position>().standPlayer = Global.player;
        Global.adjustPlayerRotationMove(Global.player, Global.desPosition, Global.isPlayerHoldsBall);
        Global.player.GetComponent<Player>().anime.Play("rig|walk");
        
        Global.isReadyToMove = true;
        if(Global.isDribble && isHavingBall)
            Global.movePoint -= 2;
        else
            Global.movePoint -= 1;
    }

    bool throwCoin()
    {
        if(UnityEngine.Random.Range(0,100)>50)
            return true;
        else
            return false;
    }

    float computeDistance(int[] posA, int[] posB)
    {
        string APos = string.Format("Pos{0}-{1}", posA[0] + 1, posA[1] + 1);
        string BPos = string.Format("Pos{0}-{1}", posB[0] + 1, posB[1] + 1);
        GameObject APosition = GameObject.Find(APos);
        GameObject BPosition = GameObject.Find(BPos);
        return Vector3.Distance(APosition.transform.localPosition, BPosition.transform.localPosition);
    }
}
