using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KArrive : GeneralBehaviour {

    public float radius = 4f, timeToTarget = 0.25f;
    //public GetNewOrientation orientation = new GetNewOrientation();

    // Use this for initialization
    new void Start () {
        base.Start();
    }

    // Update is called once per frame
    new void Update () {
        character.velocity = target.transform.position - character.transform.position;
        if (character.velocity.magnitude<radius)
        {
            return;
        }
        character.velocity /= timeToTarget;
        if (character.velocity.magnitude>character.maxSpeed)
        {
            character.velocity.Normalize();
            character.velocity *= character.maxSpeed;
        }
        character.transform.position += character.velocity * Time.deltaTime;
        //character.orientation = GetNewOrientation(character.orientation, character.velocity);
    }
}
