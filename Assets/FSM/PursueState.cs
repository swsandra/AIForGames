using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PursueState : State {

    GraphPathFollowing pathFollowing;

    int speed;

    GameObject invocant;

    public PursueState(GameObject inv, int pursueSpeed){
        //Store path following script from gameobject
        invocant = inv;
        pathFollowing = invocant.GetComponent<GraphPathFollowing>();
        if (invocant.name=="Monster_Anger") {
            pathFollowing.astar_target=GameObject.Find("Monster_Fear");
        }
        else if (invocant.name=="Monster_Sadness"){
            pathFollowing.astar_target=GameObject.Find("Monster_Happiness");
        }
    }

    public override void GetAction(){
        //Changes speed of character
        invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
        //Pursues while it is in sight triangle (not implemented yet)

        //For now it just pursues after 5s of being lonely
    }
    
    public override List<Transition> GetTransitions(){
        List<Transition> transitions= new List<Transition>();
        if (invocant.name=="Monster_Anger") {
            
        }
        else if (invocant.name=="Monster_Sadness"){
            //If some time passes
            transitions.Add(new TimePassTrans(invocant, 5));
            //Gets tired
        }
        return transitions;
    }

}