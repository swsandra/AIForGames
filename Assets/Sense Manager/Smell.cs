using UnityEngine;
using System;
using System.Collections;

public class Smell : MonoBehaviour{

	public bool scent, newScent;
	RegionalSenseManager senseManager;
	Modality smell;

    float secondsForSmell;

    DateTime startOfSmell;

	// Use this for initialization
	void Start()
	{
		scent=false;
        newScent=false;
		senseManager = GameObject.Find("Sense Manager").GetComponent<RegionalSenseManager>();
		smell=new SmellModality();
        secondsForSmell=10f;
	}

	// Update is called once per frame
	void Update()
	{
        if (newScent){
            startOfSmell=DateTime.Now;
            newScent=false;
            scent=true;
        }
		if (scent){
			//Add signal to regional sense manager
			senseManager.newSignals.Enqueue(new Signal(50, transform.position, smell));
            //Calculate time for signal to end
            float seconds = Mathf.Abs((float)((System.DateTime.Now - startOfSmell).TotalSeconds));
            if(seconds>secondsForSmell){
                scent=false;
            }
			
		}
	}

}