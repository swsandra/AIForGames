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
		return "goto";

	}

}