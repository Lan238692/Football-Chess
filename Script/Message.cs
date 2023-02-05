using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public string description;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.localPosition;
        this.transform.localPosition = Global.hide;
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.timer>=0f)
        {           
            if(description == "messageText")
            {
                this.GetComponent<Text>().text = Global.message;
            }
            this.transform.localPosition = pos;
            Global.timer -= Time.deltaTime;
        }
        else
        {
            this.transform.localPosition = Global.hide;
        }
    }
}
