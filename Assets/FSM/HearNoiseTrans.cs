using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HearNoiseTrans : Transition {

	GameObject invocant;

	Sight sight;

	Hearing hearing;

	public HearNoiseTrans(GameObject inv){
		invocant=inv;
		hearing=invocant.GetComponent<Hearing>();
	}

	public override bool IsTriggered(){
		//Check if there is any monster inside max radius of hearing
		if(hearing.heardCharacters.Count!=0){
			return true;
		}
		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		invocant.GetComponent<GraphPathFollowing>().path=new List<Node>();
		return "search";

	}

}