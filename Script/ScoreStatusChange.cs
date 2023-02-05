using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Handling the changing of match score

public class ScoreStatusChange : MonoBehaviour
{
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(description=="LeftPoint")
            this.GetComponent<TMP_Text>().text = Global.leftPoint.ToString();
        if(description=="HomeScore")
            this.GetComponent<TMP_Text>().text = Global.homeScore.ToString();
        if(description=="AwayScore")
            this.GetComponent<TMP_Text>().text = Global.awayScore.ToString();
        if(description=="Time")
            this.GetComponent<TMP_Text>().text = Global.gameTime.ToString()+":00";    
    }
}
