using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HearModality: Modality {

    public HearModality(){
        name="hearing";
        maximumRange=40;
        attenuation=1f; //FOR SMELL THIS HAS TO CHANGE
        inverseTransmissionSpeed=0f;
    }

	public override bool extraChecks(){
        return true;
    }

}