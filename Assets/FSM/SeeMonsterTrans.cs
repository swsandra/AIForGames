using UnityEngine;
using System;
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
		Debug.Log("Creating see monster");
	}

	public override bool IsTriggered(){
		//Check sight script to know if any monster gets in sight line
		if(sight.inSightCharacters.Count!=0){
			//Check if it is the target for each monster
			if (invocant.name=="Monster_Disgust") {
				
			}
			else if (invocant.name=="Monster_Anger") {
				if (sight.inSightCharacters.Contains("Monster_Fear")){
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
		if(hearing.heardCharacters.Count!=0){
			//Check if it is the target for each monster
			if (invocant.name=="Monster_Disgust") {
				
			}
			else if (invocant.name=="Monster_Anger") {
				if (hearing.heardCharacters.Contains("Monster_Fear")){
					return true;
				}
			}
			else if (invocant.name=="Monster_Sadness"){
				if (hearing.heardCharacters.Contains("Monster_Happiness")){
					return true;
				}
			}
		}

		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		return "pursue";

	}

}