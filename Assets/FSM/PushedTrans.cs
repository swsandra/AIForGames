using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushedTrans : Transition {

	GameObject invocant;

	Sight sight;

	Hearing hearing;

	public PushedTrans(GameObject inv){
		invocant=inv;
	}

	public override bool IsTriggered(){
        //If it was pushed
		if(invocant.GetComponent<Separation>().pushed){
            invocant.GetComponent<Separation>().pushed=false;
        }
		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		return "waypoint";

	}

}