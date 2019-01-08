using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PursueState : State {

    GraphPathFollowing pathFollowing;

    float speed, reachRadius=8f;

    GameObject invocant;

    List<Transition> transitions;

    public PursueState(GameObject inv, List<Transition> trans, float pursueSpeed){
        //Store path following script from gameobject
        invocant = inv;
        transitions= trans;
        name="pursue";
        pathFollowing = invocant.GetComponent<GraphPathFollowing>();
        pathFollowing.astar_target=null; //Set to null just in case
        speed = pursueSpeed;
    }

    public override void GetAction(){
        //Changes speed of character
        invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
        if (invocant.name=="Monster_Anger") {
            GameObject fear = GameObject.Find("Monster_Fear");
            pathFollowing.astar_target= fear;
            //Check if it is within certain range, it maximizes its speed
            if(Vector3.Distance(fear.transform.position,invocant.transform.position)<reachRadius){
                invocant.GetComponent<Agent>().maxSpeed = (speed*2);
                invocant.GetComponent<Agent>().maxAcc = (speed*4)+10;
            }
        }
        else if (invocant.name=="Monster_Sadness"){
            pathFollowing.astar_target=GameObject.Find("Monster_Happiness");
        }
    }
    
    public override List<Transition> GetTransitions(){
        return transitions;
    }

}