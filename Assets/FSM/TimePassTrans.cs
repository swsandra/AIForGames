using UnityEngine;
using System.Collections;
using System;

public class TimePassTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    float delay;

    public TimePassTrans(GameObject inv, float d){
        invocant=inv;
        delay=d;
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
        //For now it cheks if n sec have passed
        DateTime currentTime = System.DateTime.Now;
        if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>delay){
            return true;
        }
        return false;
    }

    public override State GetTargetState(){
        if (invocant.name=="Monster_Disgust") {
            
        }
        else if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){
            
        }
        return new PatrollState(invocant);

    }

}