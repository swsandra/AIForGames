using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PursueState : State {

    List<Transition> transitions;

    GraphPathFollowing pathFollowing;

    int speed;

    Agent character;

    public PursueState(GraphPathFollowing pFollow, List<Transition> trans, Agent charact, int pursueSpeed, GameObject target){
        //Transitions correspond to character, generated in their state machine script
        transitions=trans;
        //Store path following script from gameobject
        pathFollowing = pFollow;
        pathFollowing.astar_target=target;
    }

    public override void GetAction(){
        //Changes speed of character
        character.maxSpeed = speed;
        character.maxAcc = (speed*2)+10;
        //Pursues while it is in sight triangle (not implemented yet)

        //For now it just pursues after 5s of being lonely
    }
    
    public override List<Transition> GetTransitions(){
        return transitions;
    }

}