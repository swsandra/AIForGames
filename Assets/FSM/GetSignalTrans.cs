using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GetSignalTrans : Transition {

	GameObject invocant;

	public GetSignalTrans(GameObject inv){
		invocant=inv;
	}

	public override bool IsTriggered(){
		//Check if sensor has a new signal
        if(invocant.GetComponent<Sensor>().newSignal){
			invocant.GetComponent<Sensor>().newSignal=false;
            return true;
        }
		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		if (invocant.name=="Monster_Anger") {
            return ""; //HAS TO RETURN SOMETHING LIKE LOOK FOR SOMEONE (using max range of hearing)
        }
        else if (invocant.name=="Monster_Sadness"){
			return "eat";
        }
		return "";	

	}

}