using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Talk : MonoBehaviour{

	public bool talk;
	RegionalSenseManager senseManager;
	Modality hear;

	// Use this for initialization
	void Start()
	{
		talk=false;
		senseManager = GameObject.Find("Sense Manager").GetComponent<RegionalSenseManager>();
		hear=new HearModality();

	}

	// Update is called once per frame
	void Update()
	{
		if (talk){
			//Add signal to regional sense manager
			senseManager.newSignals.Enqueue(new Signal(40, transform.position, hear));
			talk=false;
		}
	}

}