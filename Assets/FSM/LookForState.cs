using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookForState : State {

	GraphPathFollowing pathFollowing;

	float speed;
	int newTargetNode;

	GameObject invocant;

	GraphMap graph;

	List<Transition> transitions;

	public LookForState(GameObject inv, List<Transition> trans, float lookSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="look";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		speed = lookSpeed;
		graph = GameObject.Find("Map Graph").GetComponent<GraphMap>();
		newTargetNode=-1;
		pathFollowing.astar_target=null; //Set to null just in case
	}

	public override void GetAction(){
		//Changes speed of character
		invocant.GetComponent<Agent>().maxSpeed = speed;
		invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
		if (newTargetNode==-1){
			if (invocant.name=="Monster_Anger") {
				newTargetNode = pathFollowing.graph.GetNearestNodeByCenter(GameObject.Find("Monster_Fear").transform.position);
			}
			else if (invocant.name=="Monster_Sadness"){
				newTargetNode = pathFollowing.graph.GetNearestNodeByCenter(GameObject.Find("Monster_Happiness").transform.position);
			}
		}
		//New target node is going to be an adyacent one
		if (pathFollowing.path.Count==0){
			List<int> adyacents = graph.GetNodeAdyacents(newTargetNode);
			if (invocant.name=="Monster_Anger") {
				newTargetNode = graph.GetNearestAdyacentNodeByCenter(GameObject.Find("Monster_Fear").transform.position,adyacents);
			}
			else if (invocant.name=="Monster_Sadness"){
				newTargetNode = graph.GetNearestAdyacentNodeByCenter(GameObject.Find("Monster_Happiness").transform.position,adyacents);
			}
			pathFollowing.ChangeEndNode(newTargetNode);
		}
		
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}