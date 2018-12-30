using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrollState : State{

    //Patroll region, vectors indicating limits within patroll (min and max)
    Vector3[] patrollRegion;

    List<Transition> transitions;

    public PatrollState(char region, List<Transition> trans){
        //Transitions correspond to character, generated in their state machine script
        transitions=trans;
        //If it is north, between izq arriba (-84,65), der arriba (76, 44), izq abajo (-84,-10), der abajo (76, 31)
        if (region=='n') {
            Vector3[] patrollRegion = new Vector3[2];
            patrollRegion[0] = new Vector3(-84f, -10f, 0f);
            patrollRegion[1] = new Vector3(76f, 31f, 0f);
        }//If it is south, between izq arriba (-123,-18), izq abajo (-123,-88), der abajo (127, -69), der arr (127, -23)
        else if (region=='s') {
            Vector3[] patrollRegion = new Vector3[2];
            patrollRegion[0] = new Vector3(-123f, -88f, 0f);
            patrollRegion[1] = new Vector3(127f, -18f, 0f);
        }//If it is all regions, between izq arriba (-84,65), izq abajo (-123,-88), der abajo (127, -69), der arriba (145, 62)
        else if (region=='a'){
            Vector3[] patrollRegion = new Vector3[2];
            patrollRegion[0] = new Vector3(-84f, -88f, 0f);
            patrollRegion[1] = new Vector3(127f, 65f, 0f);
        }

    }

    public override void GetAction(){
        //If path is not empty, do nothing

        //else
        //Generate numbers between min and max

        //Get node of result vector

        //Change astar target
    }
    
    public override List<Transition> GetTransitions(){
        return transitions;
    }

}