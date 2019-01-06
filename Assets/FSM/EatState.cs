using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EatState : State {

	GraphPathFollowing pathFollowing;

	GameObject invocant;

	GraphMap graph;

	List<Transition> transitions;

	public EatState(GameObject inv, List<Transition> trans){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="eat";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		graph = GameObject.Find("Map Graph").GetComponent<GraphMap>();
		pathFollowing.astar_target=null; //Set to null just in case
		Debug.Log("Creating eat state");
	}

	public override void GetAction(){
		if(invocant.GetComponent<Sensor>().targetNode!=-1){
			if (pathFollowing.path.Count!=0){
				pathFollowing.ChangeEndNode(invocant.GetComponent<Sensor>().targetNode);
			}
			
		}
		return;
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}