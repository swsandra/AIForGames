using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReachTrans : Transition {

	GameObject invocant;

    float reachRadius = 6f;

	public ReachTrans(GameObject inv){
		invocant=inv;
	}

	public override bool IsTriggered(){
		//Check if is within reach radius
        GameObject fear = GameObject.Find("Monster_Fear");
        if(Vector3.Distance(fear.transform.position,invocant.transform.position)<reachRadius){
            return true;
        }

		return false;
	}

	public override string GetTargetState(){

		invocant.GetComponent<GraphPathFollowing>().astar_target=null; //Just in case
		return "push";

	}

}