using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KFlee : GeneralBehaviour {

    // Use this for initialization
    new void Start () {
        base.Start();
    }
    // Update is called once per frame
    void Update () {
        character.velocity = character.transform.position - target.transform.position;
        character.velocity.Normalize();
        character.velocity *= character.maxSpeed;
        character.orientation = GetNewOrientation(character.orientation, character.velocity);
        
        //character.transform.Rotate(character.velocity, Space.World); //VOLAR

        //character.transform.Translate(character.velocity,Space.Self);
        //print(character.velocity);

    }
}
