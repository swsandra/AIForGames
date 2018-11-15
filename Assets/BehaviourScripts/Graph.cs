using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph{

    //List of Nodes
    public List<Node> nodes;

    //List of Connections
    public List<Connection> connections;

    public bool AddNode(Vector3[] vertex, int id){

        foreach (Node node in nodes){
            if (node.vertex.Equals(vertex) || node.id==id){
                return false;
            }
        }

        nodes.Add(new Node(vertex,id));
        return true;

    }

    //Add Connection
    public bool AddConnection(Node initial, Node final, float cost){

        foreach (Connection con in connections){
            if (con.initialNode.id==initial.id && con.finalNode.id==final.id){
                return false;
            }
        }

        connections.Add(new Connection(initial, final, cost));
        return true;

    }


    //Find sucesors of a node


    //Find predecesors of a node




}