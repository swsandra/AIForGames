using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GraphMap{

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
    public List<Node> Sucesors(Node node){
        
        List<Node> sucesors = new List<Node>();
        foreach(Connection con in connections){
            if (con.initialNode.Equals(node)){
                sucesors.Add(con.finalNode);
            }
        }
        return sucesors;

    }

    //Find predecesors of a node
    public List<Node> Predecesors(Node node){
        
        List<Node> predecesors = new List<Node>();
        foreach(Connection con in connections){
            if (con.finalNode.Equals(node)){
                predecesors.Add(con.initialNode);
            }
        }
        return predecesors;

    }

    //Information for each node
    public struct NodeRecord{

        public Node node;
        public Connection connection;
        public float costSoFar;
        public float estimatedTotalCost;

        public NodeRecord(Node node, Connection connection, float costSoFar, float estimatedTotalCost){
            this.node=node;
            this.connection=connection;
            this.costSoFar=costSoFar;
            this.estimatedTotalCost=estimatedTotalCost;
        }

    }

    public List<Node> AStar(Node start, Node end){

        List<Node> path = new List<Node>();
        path.Add(start);

        NodeRecord startRecord = new NodeRecord(start, null, 0f, EuclideanDistance(start, end));
        List<NodeRecord> open = new List<NodeRecord>();
        List<NodeRecord> closed = new List<NodeRecord>();
        open.Add(startRecord);

        while(open.Count>0f){
            NodeRecord current = SmallestElement(open);
            path.Add(current.node);
            if (current.node.Equals(end)){
                return path;
            }


        }

    }

    public float EuclideanDistance(Node start, Node end){

        

    }

    public NodeRecord SmallestElement(List<NodeRecord> records){

        NodeRecord smallest=records.First();
        foreach(NodeRecord record in records){
            if (record.estimatedTotalCost<smallest.estimatedTotalCost){
                smallest=record;
            }
        }
        return smallest;

    }

}