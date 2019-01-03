using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HearModality: Modality {

    public HearModality(){
        name="hearing";
        minimumRange=15;
        maximumRange=30;
        attenuation=0.5f;
        inverseTransmissionSpeed=1f;
    }

	public override bool extraChecks(){
        return true;
    }

}