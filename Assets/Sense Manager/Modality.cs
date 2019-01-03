using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Modality{

	public int minimumRange, maximumRange;

	public string name;

	public float attenuation, inverseTransmissionSpeed;

	public abstract bool extraChecks();

}