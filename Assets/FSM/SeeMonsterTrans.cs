using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SeeMonsterTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    bool changeCreationTime;

    public SeeMonsterTrans(GameObject inv){
        invocant=inv;
        //Save creation time
        //creationTime = System.DateTime.Now;
        changeCreationTime = true;
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){

        }
        Debug.Log("Creating see monster");
    }

    public override bool IsTriggered(){
        //Check if any monster gets in sight line
        //For now it cheks if 5 sec have passed
        if (changeCreationTime){
            creationTime = System.DateTime.Now;
            changeCreationTime=false;
        }
        DateTime currentTime = System.DateTime.Now;
        float seconds = Mathf.Abs((float)((currentTime - creationTime).TotalSeconds));
        if(seconds>12f){
            return true;
        }
        return false;
    }

    public override string GetTargetState(){
        if (invocant.name=="Monster_Disgust") {
            //If the monster it saw was happiness

            //If it was fear
        }
        else if (invocant.name=="Monster_Anger") {
            //Pursue fear
        }
        else if (invocant.name=="Monster_Sadness"){
            //Pursue happiness
        }

        invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
        changeCreationTime=true;
        return "pursue";

    }

}