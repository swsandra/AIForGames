using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FoodDisappearsTrans : Transition{

    GameObject invocant;

    DateTime creationTime;

    bool changeCreationTime;

    Smell smell;

    public FoodDisappearsTrans(GameObject inv){
        invocant=inv;
        changeCreationTime=true;
        smell=GameObject.Find("Sushi").GetComponent<Smell>();
    }

    public override bool IsTriggered(){
        
        if (!smell.sushiSprite.enabled){
            if (changeCreationTime){
                creationTime = System.DateTime.Now;
                changeCreationTime=false;
            }
            DateTime currentTime = System.DateTime.Now;
            if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>2f){
                Debug.Log("Two seconds passed.");
                return true;
            }
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
        invocant.GetComponent<GraphPathFollowing>().path=new List<Node>();
        changeCreationTime=true;
        return "patroll";

    }

}