using UnityEngine;
using System.Collections;

public class Connection{

    //Cost
    public float cost;
    
    //Initial node and final node
    public Node initialNode, finalNode;

    public Connection(Node initialNode, Node finalNode, float cost){

        this.initialNode=initialNode;
        this.finalNode=finalNode;
        this.cost=cost;

    }

}