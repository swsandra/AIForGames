using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimePassTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    float delay;

    bool changeCreationTime;

    public TimePassTrans(GameObject inv, float d){
        invocant=inv;
        delay=d;
        changeCreationTime=false;
        //Save creation time
        creationTime = System.DateTime.Now;
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){

        }
        Debug.Log("Creating time pass transition");
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
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){
            
        }

        invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
        changeCreationTime=true;
        return "patroll";

    }

}