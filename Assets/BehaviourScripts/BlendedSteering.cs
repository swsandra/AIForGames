using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlendedSteering : GeneralBehaviour
{

    public List<BehaviourAndWeight> behaviours;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        //GameObject gb = new GameObject("GeneralBehaviour");
        //BehaviourAndWeight behav = new BehaviourAndWeight(gb, 2);
        //behaviours.Add(behav);
        /*Pursue pursue = new Pursue();
        BehaviourAndWeight behav = new BehaviourAndWeight(pursue, 2);
        behaviours.Add(behav);
        LookWhereYoureGoing lwyag = new LookWhereYoureGoing();
        behav = new BehaviourAndWeight(lwyag, 2);
        behaviours.Add(behav);
        ObstacleAvoidance oa = new ObstacleAvoidance();
        behav = new BehaviourAndWeight(oa, 3);
        behaviours.Add(behav);*/

    }

    // Update is called once per frame
    void Update()
    {
        //character.steering = GetSteering();
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        steering = new Steering();
        /*foreach (BehaviourAndWeight behaviour in behaviours)
        {
            //Set character and target
            behaviour.behaviour.character = character;
            //Set targets for Separation
            if (behaviour.GetType() == typeof(Separation)) //Align (takes float targetRotation)
            {
                behaviour.behaviour.separationTargets = separationTargets;
            }
            else
            {
                behaviour.behaviour.target = target;
            }

            //All GetSteering functions take no parameters except for Align and Face
            if (behaviour.GetType() == typeof(Align)) //Align (takes float targetRotation)
            {
                Steering newSteer = behaviour.behaviour.GetSteering(target.transform.rotation.eulerAngles.z);
                steering.angular += behaviour.weight * newSteer.angular;
            }
            else if (behaviour.GetType() == typeof(Face)) //Face (vector3 targetDirection)
            {
                Steering newSteer = behaviour.behaviour.GetSteering(target.transform.position);
                steering.angular += behaviour.weight * newSteer.angular;
            }
            else //Other behaviour
            {
                Steering newSteer = behaviour.behaviour.GetSteering();
                steering.linear += behaviour.weight * newSteer.linear;
                steering.angular += behaviour.weight * newSteer.angular;
            }

        }

        //Crop to max acceleration and max angular acceleration
        if (steering.linear.magnitude>character.maxAcc)
        {
            steering.linear = steering.linear.normalized * character.maxAcc;
        }
        if (steering.angular > character.maxAngularAcc)
        {
            steering.angular = character.maxAngularAcc;
        }
        */
        foreach (BehaviourAndWeight behaviour in behaviours)
        {
            //Set character and target
            behaviour.behaviour.GetComponent<GeneralBehaviour>().character = character;
            //Set targets for Separation
            if (behaviour.GetType() == typeof(Separation)) //Align (takes float targetRotation)
            {
                behaviour.behaviour.GetComponent<GeneralBehaviour>().separationTargets = separationTargets;
            }
            else
            {
                behaviour.behaviour.GetComponent<GeneralBehaviour>().target = target;
            }

            //All GetSteering functions take no parameters except for Align and Face
            if (behaviour.GetType() == typeof(Align)) //Align (takes float targetRotation)
            {
                Steering newSteer = behaviour.behaviour.GetComponent<GeneralBehaviour>().GetSteering(target.transform.rotation.eulerAngles.z);
                steering.angular += behaviour.weight * newSteer.angular;
            }
            else if (behaviour.GetType() == typeof(Face)) //Face (vector3 targetDirection)
            {
                Steering newSteer = behaviour.behaviour.GetComponent<GeneralBehaviour>().GetSteering(target.transform.position);
                steering.angular += behaviour.weight * newSteer.angular;
            }
            else //Other behaviour
            {
                Steering newSteer = behaviour.behaviour.GetComponent<GeneralBehaviour>().GetSteering();
                steering.linear += behaviour.weight * newSteer.linear;
                steering.angular += behaviour.weight * newSteer.angular;
            }

        }

        //Crop to max acceleration and max angular acceleration
        if (steering.linear.magnitude > character.maxAcc)
        {
            steering.linear = steering.linear.normalized * character.maxAcc;
        }
        if (steering.angular > character.maxAngularAcc)
        {
            steering.angular = character.maxAngularAcc;
        }

        return steering;
    }
}
