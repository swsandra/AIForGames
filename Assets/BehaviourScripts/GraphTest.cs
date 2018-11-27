using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GraphTest : MonoBehaviour{

    //For graph which doesnt inherit from monobehaviour
    GraphMap graph;
    Node node0;
    Node node1;
    Node node2;
    Node node3;
    Node node4;
    Node node5;
    Node node6;
    Node node7;
    Node node8;
    Node node9;
    Node node10;
    Node node11;

    public bool aStar;

    void Start(){
        aStar=true;

        graph = new GraphMap();

        node0 = new Node(new Vector3(5f, -13f, 0f), 0);
        node1 = new Node(new Vector3(11f, -9.5f, 0f), 1);
        node2 = new Node(new Vector3(11f, -5f, 0f), 2);
        node3 = new Node(new Vector3(-3f, -12.4f, 0f), 3);
        node4 = new Node(new Vector3(-3f, -7.2f, 0f), 4);
        node5 = new Node(new Vector3(4.2f, -5f, 0f), 5);
        node6 = new Node(new Vector3(11f, 0f, 0f), 6);
        node7 = new Node(new Vector3(6f, 1.5f, 0f), 7);
        node8 = new Node(new Vector3(19f, -1f, 0f), 8);
        node9 = new Node(new Vector3(19f, -9.5f, 0f), 9);
        node10 = new Node(new Vector3(-11f, -12.4f, 0f), 10);
        node11 = new Node(new Vector3(-11f, -7f, 0f), 11);

        graph.AddTestNode(node0);
        graph.AddTestNode(node1);
        graph.AddTestNode(node2);
        graph.AddTestNode(node3);
        graph.AddTestNode(node4);
        graph.AddTestNode(node5);
        graph.AddTestNode(node6);
        graph.AddTestNode(node7);
        graph.AddTestNode(node8);
        graph.AddTestNode(node9);
        graph.AddTestNode(node10);
        graph.AddTestNode(node11);

        graph.AddConnection(node0,node1);
        graph.AddConnection(node0,node3);
        graph.AddConnection(node10,node3);
        graph.AddConnection(node4,node3);
        graph.AddConnection(node2,node3);
        graph.AddConnection(node2,node1);
        graph.AddConnection(node4,node11);
        graph.AddConnection(node4,node5);
        graph.AddConnection(node9,node1);
        graph.AddConnection(node2,node8);
        graph.AddConnection(node2,node6);
        graph.AddConnection(node5,node6);
        graph.AddConnection(node5,node7);
        graph.AddConnection(node6,node7);    
    }
    
    // Update is called once per frame
    new void Update()
    {
        //Debug.Log("hola");
        if (aStar){
            //Debug.Log("hola");
            List<Node> path = graph.AStar(node0,node7);
            foreach (Node node in path)
            {
                Debug.Log(node.id);
            }
            aStar=false;
        }
    }

    

    
}