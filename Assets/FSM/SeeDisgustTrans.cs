using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeeDisgustTrans : Transition {

	GameObject invocant;

	Sight sight;

	Hearing hearing;

	public SeeDisgustTrans(GameObject inv){
		invocant=inv;
		sight=invocant.GetComponent<Sight>();
		hearing=invocant.GetComponent<Hearing>();
	}

	public override bool IsTriggered(){
		//Check sight script to know if any monster gets in sight line
		if(sight.inSightCharacters.Count!=0){
            if (sight.inSightCharacters.Contains("Monster_Disgust")){
					return true;
			}
		}
		//Check if there is any monster inside min radius in hearing
		if(hearing.heardCloseCharacters.Count!=0){
			if (hearing.heardCloseCharacters.Contains("Monster_Disgust")){
					return true;
			}
		}

		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		return "pursue";

	}

}