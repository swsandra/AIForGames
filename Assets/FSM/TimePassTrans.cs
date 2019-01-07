using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimePassTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    float delay;

    bool changeCreationTime;
    
    string nextTransition;

    public TimePassTrans(GameObject inv, float d, string transition){
        invocant=inv;
        delay=d;
        nextTransition=transition;
        changeCreationTime=true;
        //Save creation time
        //creationTime = System.DateTime.Now;
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){

        }
    }

    public override bool IsTriggered(){
        //For now it cheks if n sec have passed
        if (changeCreationTime){
            creationTime = System.DateTime.Now;
            changeCreationTime=false;
        }
        DateTime currentTime = System.DateTime.Now;
        if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>delay){
            return true;
        }
        return false;
    }

    public override string GetTargetState(){
        invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
        invocant.GetComponent<GraphPathFollowing>().path=new List<Node>();
        changeCreationTime=true;
        return nextTransition;

    }

}