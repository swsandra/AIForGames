using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeeMonsterTrans : Transition {

	GameObject invocant;

	Sight sight;

	Hearing hearing;

	public SeeMonsterTrans(GameObject inv){
		invocant=inv;
		sight=invocant.GetComponent<Sight>();
		hearing=invocant.GetComponent<Hearing>();
	}

	public override bool IsTriggered(){
		//Check sight script to know if any monster gets in sight line
		if(sight.inSightCharacters.Count!=0){
			//Check if it is the target for each monster
			if (invocant.name=="Monster_Fear") {
				if (sight.inSightCharacters.Contains("Monster_Anger")){
					return true;
				}
			}
			else if (invocant.name=="Monster_Anger") {
				if (sight.inSightCharacters.Contains("Monster_Fear")){
					return true;
				}
			}
			else if (invocant.name=="Monster_Disgust") {
				if (sight.inSightCharacters.Contains("Monster_Fear")){
					GameObject.Find("Monster_Disgust").GetComponent<Talk>().target=GameObject.Find("Monster_Fear").transform.position;
					return true;
				}
			}
			else if (invocant.name=="Monster_Sadness"){
				if (sight.inSightCharacters.Contains("Monster_Happiness")){
					return true;
				}
			}
		}
		//Check if there is any monster inside min radius in hearing
		if(hearing.heardCloseCharacters.Count!=0){
			//Check if it is the target for each monster
			if (invocant.name=="Monster_Fear") {
				if (hearing.heardCloseCharacters.Contains("Monster_Anger")){
					return true;
				}
			}
			else if (invocant.name=="Monster_Anger") {
				if (hearing.heardCloseCharacters.Contains("Monster_Fear")){
					return true;
				}
			}
			else if (invocant.name=="Monster_Disgust") {
				if (hearing.heardCloseCharacters.Contains("Monster_Fear")){
					GameObject.Find("Monster_Disgust").GetComponent<Talk>().target=GameObject.Find("Monster_Fear").transform.position;
					return true;
				}
			}
			else if (invocant.name=="Monster_Sadness"){
				if (hearing.heardCloseCharacters.Contains("Monster_Happiness")){
					return true;
				}
			}
		}

		return false;
	}

	public override string GetTargetState(){

		if (invocant.name=="Monster_Fear") {
			return "waypoint";
		}
		else if (invocant.name=="Monster_Disgust") {
			return "scream";
		}
		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		return "pursue";

	}

}