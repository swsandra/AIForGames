using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchState : State {

    GraphPathFollowing pathFollowing;

    float speed;

    GameObject invocant;

    List<Transition> transitions;

    public SearchState(GameObject inv, List<Transition> trans, float pursueSpeed){
        //Store path following script from gameobject
        invocant = inv;
        transitions= trans;
        name="search";
        pathFollowing = invocant.GetComponent<GraphPathFollowing>();
        pathFollowing.astar_target=null; //Set to null just in case
        speed = pursueSpeed;
    }

    public override void GetAction(){
        //Changes speed of character
        invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;

        List<string> heardCharacters = invocant.GetComponent<Hearing>().heardCharacters;
        if(heardCharacters.Count!=0){
            Vector3 target = GameObject.Find(heardCharacters[0]).transform.position;
            //Check hearing script to look for end node of heard character
            pathFollowing.ChangeEndNode(pathFollowing.graph.GetNearestNodeByCenter(target));
        }
    }
    
    public override List<Transition> GetTransitions(){
        return transitions;
    }

}