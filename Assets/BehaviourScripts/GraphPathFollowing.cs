using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphPathFollowing : GeneralBehaviour
{

    //Graph
    GraphMap graph;
    Node node0, node1, node2, node3, node4, node5, node6, node7, node8, node9, node10, node11;
    public Node initial, end, current;

    //Vector target position
    Vector3 targetPosition;
    
    //Radius to change from nodes, target radius and slow radius
    public float changeRadius = 1f, tRadius= 2f, sRadius = 3f, timeToTarget = 0.2f;

    //If it is in final node
    public bool final;

    //Path to follow
    List<Node> path;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        graph = new GraphMap();

        node0 = new Node(new Vector3(-1f, -54.9f, 0f), 0);
        node1 = new Node(new Vector3(22.6f, -35f, 0f), 1);
        node2 = new Node(new Vector3(23.8f, -15.4f, 0f), 2);
        node3 = new Node(new Vector3(-25.7f, -28.3f, 0f), 3);
        node4 = new Node(new Vector3(-24.9f, -1.4f, 0f), 4);
        node5 = new Node(new Vector3(1.5f, 8f, 0f), 5);
        node6 = new Node(new Vector3(30.1f, 14.3f, 0f), 6);
        node7 = new Node(new Vector3(2.5f, 39.2f, 0f), 7);
        node8 = new Node(new Vector3(51.6f, -2.7f, 0f), 8);
        node9 = new Node(new Vector3(52.4f, -31.8f, 0f), 9);
        node10 = new Node(new Vector3(-67.7f, -26f, 0f), 10);
        node11 = new Node(new Vector3(-59.4f, -1.9f, 0f), 11);

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
        
        //This needs to be changed when implemented with triangles to calculate nearest node
        //(end can be set in state machine script of agent)
        initial=node0;
        end=node7;
        path = graph.AStar(initial,end);
        current = path[0];
        path.RemoveAt(0);

    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(path), weight, priority);
    
    }

    public Steering GetSteering(List<Node> path)
    {
        //If it is at a certain radius from last current, change
        Vector3 distanceToNode = current.center-character.transform.position;
        //Debug.Log("current node "+ current.id +" "+current.center);
        //Debug.Log("distance to node "+distanceToNode.magnitude);
        if (distanceToNode.magnitude<changeRadius && path.Count!=0){
            //Remove first element
            current = path[0];
            path.RemoveAt(0);
            //If path is not empty, seek
            if (path.Count!=0){
                targetPosition = current.center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }
            else{ //else arrive at current node
                targetPosition = current.center;
                Vector3 direction = targetPosition - character.transform.position; 
                float distance = direction.magnitude; 
                float tSpeed;

                if (distance < tRadius)
                {
                    steering.linear = Vector3.zero; 
                    character.velocity = Vector3.zero;    
                    return steering;
                }

                if (distance > sRadius)
                {
                    tSpeed = character.maxSpeed;
                }
                else
                {
                    tSpeed = character.maxSpeed * (distance / sRadius);
                }
                Vector3 tVelocity = direction.normalized * tSpeed;

                steering.linear = tVelocity - character.velocity;
                
                steering.linear /= timeToTarget;

                if (steering.linear.magnitude > character.maxAcc)
                {
                    steering.linear.Normalize();
                    steering.linear *= character.maxAcc;
                }

                steering.angular = 0f;
            }
        }
        else{//else seek current
            //Debug.Log("Current node "+ current.id);
            if(current.id!=end.id){ //seek last current
                targetPosition = current.center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }else{ //arrive at final node
                targetPosition = current.center;
                Vector3 direction = targetPosition - character.transform.position; 
                float distance = direction.magnitude; 
                float tSpeed;

                if (distance < tRadius)
                {
                    steering.linear = Vector3.zero; 
                    character.velocity = Vector3.zero;    
                    return steering;
                }

                if (distance > sRadius)
                {
                    tSpeed = character.maxSpeed;
                }
                else
                {
                    tSpeed = character.maxSpeed * (distance / sRadius);
                }
                Vector3 tVelocity = direction.normalized * tSpeed;

                steering.linear = tVelocity - character.velocity;
                
                steering.linear /= timeToTarget;

                if (steering.linear.magnitude > character.maxAcc)
                {
                    steering.linear.Normalize();
                    steering.linear *= character.maxAcc;
                }

                steering.angular = 0f;
            }
            
        }
        return steering;

    }

    //To use this, GetComponent
    public void ChangeEndNode(Node node){
        //Remove first element of path, is now start
        initial = path[0];
        path.RemoveAt(0);
        end = node;
        //arrive=false;
        path = graph.AStar(initial, end);
    }

}