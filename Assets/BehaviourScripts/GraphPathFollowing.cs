using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GraphPathFollowing : GeneralBehaviour
{

    //Graph
    GraphMap graph;
    //Node node0, node1, node2, node3, node4, node5, node6, node7, node8, node9, node10, node11;
    public int initial, end, current;

    //Vector target position
    Vector3 targetPosition;
    
    //Radius to change from nodes, target radius and slow radius
    public float changeRadius = 1f, tRadius= 2f, sRadius = 3f, timeToTarget = 0.2f;

    //Script for final node
    GeneralBehaviour behaviour;

    //Path to follow
    List<Node> path;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        //Get map graph
        GameObject map = GameObject.Find("Map Graph");
        graph = map.GetComponent<GraphMap>();

        initial=GetNearestNode(character.transform.position);

        //Pursue 
        end = GetNearestNode(target.transform.position);
        //end = graph.nodes[0];
        path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
        behaviour = GetComponent<DArrive>();
        current = path[0].id;
        path.RemoveAt(0);
    }

    // Update is called once per frame
    new void Update()
    {
        int targetNode = GetNearestNode(target.transform.position);
        int characterNode = GetNearestNode(character.transform.position);
        graph.nodes[initial].DrawTriangle();
        graph.nodes[end].DrawTriangle();
        //Debug.Log(graph.nodes.ElementAt(end).Value.id);
        //Debug.DrawLine(Vector3.zero, initial.center, Color.white);
        //Debug.DrawLine(character.transform.position, characterNode.center, Color.red);
        //Debug.Log("character "+character.transform.position+" current "+current.center);
        if (targetNode!=characterNode){
            initial = characterNode;
            end = targetNode;
            path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
            character.SetSteering(GetSteering(path), weight, priority);
        }
    }

    public Steering GetSteering(List<Node> path)
    {
        //If it is at a certain radius from last current, change 
        Vector3 distanceToNode = graph.nodes[current].center-character.transform.position;
        if (distanceToNode.magnitude<changeRadius && path.Count!=0){
            //Remove first element
            current = path[0].id;
            path.RemoveAt(0);
            //If path is not empty, seek
            if (path.Count!=0){
                targetPosition = graph.nodes[current].center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }
            else{ //else arrive at current node
                targetPosition = graph.nodes[current].center;
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
            if(current!=end){ //seek last current
                targetPosition = graph.nodes[current].center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }else{ //arrive at final node
                targetPosition = graph.nodes[current].center;
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
    public void ChangeEndNode(int node){
        initial = GetNearestNode(character.transform.position);
        end = node;
        path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
    }

    public int GetNearestNode(Vector3 position){
        int nearestNode=-1;
        foreach(KeyValuePair<int, Node> entry in graph.nodes)
        {
            Vector3 A = entry.Value.vertex[0];
            Vector3 B = entry.Value.vertex[1];
            Vector3 C = entry.Value.vertex[2];
            //Calculate areas
            float ABC = (1f/2f)* Mathf.Abs(((A.x-C.x)*(B.y-A.y))-((A.x-B.x)*(C.y-A.y)));
            float ABP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(B.y-A.y))-((A.x-B.x)*(position.y-A.y)));
            float BCP = (1f/2f)* Mathf.Abs(((B.x-position.x)*(C.y-B.y))-((B.x-C.x)*(position.y-B.y)));
            float ACP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(C.y-A.y))-((A.x-C.x)*(position.y-A.y)));
            if (ABP + BCP + ACP < ABC){
                nearestNode=entry.Value.id;
                //Debug.Log("Found");
            }
        }
        return nearestNode;
    }

}