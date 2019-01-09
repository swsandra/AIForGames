using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScreamState : State {

	GraphPathFollowing pathFollowing;

	float speed;

    int targetNode;

	GameObject invocant;

	List<Transition> transitions;

	public ScreamState(GameObject inv, List<Transition> trans, float pursueSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="scream";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
		speed = pursueSpeed;
        //targetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(4f,-59f,0f));
	}

	public override void GetAction(){
		//Changes speed of character
		invocant.GetComponent<Agent>().maxSpeed = speed;
		invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
        //targetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(4f,-59f,0f));
		targetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(16f,-49f,0f));
        pathFollowing.ChangeEndNode(targetNode);
        if(pathFollowing.path.Count==0){
            //Activate talk behaviour
            invocant.GetComponent<Talk>().talk=true;
        }        
		
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}