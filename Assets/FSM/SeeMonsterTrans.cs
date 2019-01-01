using UnityEngine;
using System.Collections;
using System;

public class SeeMonsterTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    public SeeMonsterTrans(GameObject inv){
        invocant=inv;
        //Save creation time
        creationTime = System.DateTime.Now;
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){

        }
    }

    public override bool IsTriggered(){
        //Check if any monster gets in sight line
        
        //For now it cheks if 5 sec have passed
        DateTime currentTime = System.DateTime.Now;
        if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>5f){
            return true;
        }
        return false;
    }

    public override State GetTargetState(){
        int speed=0;
        if (invocant.name=="Monster_Disgust") {
            //If the monster it saw was happiness

            //If it was fear
        }
        else if (invocant.name=="Monster_Anger") {
            //Pursue fear
            speed=12;
        }
        else if (invocant.name=="Monster_Sadness"){
            //Pursue happiness
            speed=9;
        }
        return new PursueState(invocant, speed);

    }

}