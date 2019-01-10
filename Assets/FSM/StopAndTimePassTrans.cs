using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StopAndTimePassTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    float delay;

    bool changeCreationTime;
    
    string nextTransition;

    public StopAndTimePassTrans(GameObject inv, float d, string transition){
        invocant=inv;
        delay=d;
        nextTransition=transition;
        changeCreationTime=true;
        //Save creation time
        //creationTime = System.DateTime.Now;
    }

    public override bool IsTriggered(){
        //If it has no path, wait until time has passed
        if(invocant.GetComponent<GraphPathFollowing>().path.Count==0){
            //For now it cheks if n sec have passed
            if (changeCreationTime){
                creationTime = System.DateTime.Now;
                changeCreationTime=false;
            }
            DateTime currentTime = System.DateTime.Now;
            if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>delay){
                Debug.Log(delay+" seconds have passed.");
                return true;
            }
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