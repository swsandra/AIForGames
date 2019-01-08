using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Talk : MonoBehaviour{

	public bool talk;

	public Vector3 target;
	RegionalSenseManager senseManager;
	Modality hear;

	GameObject message;

	// Use this for initialization
	void Start()
	{
		talk=false;
		senseManager = GameObject.Find("Sense Manager").GetComponent<RegionalSenseManager>();
		hear=new HearModality();
		target=Vector3.zero;
		message=GameObject.Find("Scream");
	}

	// Update is called once per frame
	void Update()
	{
		if (talk){
			//Add signal to regional sense manager
			senseManager.newSignals.Enqueue(new Signal(40, transform.position, hear));
			message.transform.position=new Vector3(transform.position.x,transform.position.y+15f,transform.position.z);
			message.GetComponent<Renderer>().enabled=true;
			//talk=false;//Scream until anger is near
		}else{
			message.GetComponent<Renderer>().enabled=false;
		}
	}

	public void ChangeTarget(Vector3 newTarget){
		target=newTarget;
	}

}