using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Smell : MonoBehaviour{

	public bool scent;
	RegionalSenseManager senseManager;
	Modality smell;

	// Use this for initialization
	void Start()
	{
		scent=false;
		senseManager = GameObject.Find("Sense Manager").GetComponent<RegionalSenseManager>();
		smell=new SmellModality();

	}

	// Update is called once per frame
	void Update()
	{
		if (scent){
			//Add signal to regional sense manager
			senseManager.newSignals.Enqueue(new Signal(50, transform.position, smell));
			scent=false;
		}
	}

}