using UnityEngine;
using System.Collections;

public class BlendedSteering : GeneralBehaviour
{

    [SerializeField] public BehaviourAndWeight[] behaviours;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        steering = new Steering();
        foreach (BehaviourAndWeight behaviour in behaviours)
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

        return steering;
    }
}
