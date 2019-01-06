using UnityEngine;
using System;
using System.Collections;

public class Smell : MonoBehaviour{

	public bool scent, newScent;
	RegionalSenseManager senseManager;
	Modality smell;

    float secondsForSmell, secondsForNewSignal;

    DateTime startOfSmell, newSignal;

	SpriteRenderer sushiSprite, ramenSprite;

	// Use this for initialization
	void Start()
	{
		scent=false;
        newScent=false;
		senseManager = GameObject.Find("Sense Manager").GetComponent<RegionalSenseManager>();
		smell=new SmellModality();
        secondsForSmell=20f;
		secondsForNewSignal = 60f;
		//TEST
		secondsForNewSignal = 5f;
		//
		newSignal=DateTime.Now;
		//Hide sprites when starting
		sushiSprite=GetComponent<SpriteRenderer>();
		ramenSprite=GameObject.Find("Ramen").GetComponent<SpriteRenderer>();
		sushiSprite.enabled=false;
		ramenSprite.enabled=false;
	}

	// Update is called once per frame
	void Update()
	{
		//Check if it is time for new scent
		DateTime current = DateTime.Now;
		float secondsNewSignal = Mathf.Abs((float)((System.DateTime.Now - newSignal).TotalSeconds));
		if(secondsNewSignal>secondsForNewSignal){
			newSignal=DateTime.Now; //So it doesnt keep entering the conditional
			newScent=true;
		}

		/*if(!newScent){
			Debug.Log(secondsNewSignal+" have passed for next signal.");
		} */

        if (newScent){
            startOfSmell=DateTime.Now;
            newScent=false;
            scent=true;
			sushiSprite.enabled=true;
			ramenSprite.enabled=true;
        }
		if (scent){
			//Add signal to regional sense manager
			senseManager.newSignals.Enqueue(new Signal(50, transform.position, smell)); //Since this is smell, each time strenght can be decreased
            //Calculate time for signal to end
            float seconds = Mathf.Abs((float)((System.DateTime.Now - startOfSmell).TotalSeconds));
			//Debug.Log(seconds+" have passed to reset new signal.");
            if(seconds>secondsForSmell){
				newSignal=DateTime.Now;
                scent=false;
				//THIS SHOULD BE CHANGED AFTER MONSTER EATS
				sushiSprite.enabled=false;
				ramenSprite.enabled=false;
            }
			
		}
	}

}