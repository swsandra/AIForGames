using UnityEngine;
using System.Collections;

public class Connection{

    //Cost
    float cost;
    
    //Initial node and final node
    Node initialNode, finalNode;

    public Connection(Node initialNode, Node finalNode, float cost){

        this.initialNode=initialNode;
        this.finalNode=finalNode;
        this.cost=cost;

    }

}