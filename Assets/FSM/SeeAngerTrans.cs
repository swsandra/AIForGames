using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SeeAngerTrans : Transition {

	GameObject invocant;

	GameObject anger;

    bool changeCreationTime;

    DateTime creationTime;

	public SeeAngerTrans(GameObject inv){
		invocant=inv;
        anger=GameObject.Find("Monster_Anger");
        changeCreationTime=true;
	}

	public override bool IsTriggered(){
        //If Anger is within radius
        if(Vector3.Distance(invocant.transform.position,anger.transform.position)<10f){
            invocant.GetComponent<Talk>().talk=false;
        }
        if(Vector3.Distance(invocant.transform.position,anger.transform.position)<4f){
            if (changeCreationTime){
                creationTime = System.DateTime.Now;
                changeCreationTime=false;
            }
            DateTime currentTime = System.DateTime.Now;
            if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>3f){ 
                return true;
            }
        }
		
        return false;
	}

	public override string GetTargetState(){

		return "sleep";

	}

}