using UnityEngine;
using System.Collections;

public class SmellModality: Modality {

    public SmellModality(){
        name="smell";
        maximumRange=100;
        attenuation=2f;
        inverseTransmissionSpeed=0.03f;
    }

	public override bool extraChecks(){
        return true;
    }

}