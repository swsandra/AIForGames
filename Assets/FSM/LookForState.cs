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
        //Changes speed of character
        invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
    }

    public override void GetAction(){
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
                newTargetNode = GetNearestAdyacentNodeByCenter(GameObject.Find("Monster_Fear").transform.position,adyacents);
            }
            else if (invocant.name=="Monster_Sadness"){
                newTargetNode = GetNearestAdyacentNodeByCenter(GameObject.Find("Monster_Happiness").transform.position,adyacents);
            }            
            pathFollowing.ChangeEndNode(newTargetNode);
		}
        
    }
    
    public int GetNearestAdyacentNodeByCenter(Vector3 position, List<int> adyacents){
		//int nearestNode=-1;
		int nearestNode = pathFollowing.graph.nodes[adyacents[0]].id;
		float smallestLine = (position - pathFollowing.graph.nodes[adyacents[0]].center).magnitude;

		for(int i = 1; i<adyacents.Count ; i++){
            Vector3 nodeCenter = pathFollowing.graph.nodes[adyacents[i]].center;
            float distance = (position - nodeCenter).magnitude;
            if (distance <= smallestLine){
                nearestNode=adyacents[i];
                smallestLine = distance;
            }
		}
		return nearestNode;
	}

    public override List<Transition> GetTransitions(){
        return transitions;
    }

}