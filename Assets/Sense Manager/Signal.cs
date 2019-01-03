using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Signal{

	public float strenght;

	public Vector3 position;

	public Modality modality;

    public Signal(float st, Vector3 pos, Modality mod){
        strenght = st;
        position=pos;
        modality=mod;
    }


}