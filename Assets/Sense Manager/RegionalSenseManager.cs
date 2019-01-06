using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RegionalSenseManager : MonoBehaviour{

    //Notification struct
    struct Notification{
        public DateTime time;
        public string sensorName;
        public Signal signal;

        public Notification(DateTime t, string sens, Signal s){
            time=t;
            sensorName=sens;
            signal=s;
        }

    }

    List<GameObject> sensors;

    Queue<Notification> notifications;

    public Queue<Signal> newSignals;
	
	 // Use this for initialization
	void Start()
	{
		GameObject characters = GameObject.Find("Characters");
        sensors=new List<GameObject>();
        foreach (Transform child in characters.transform){
            Sensor childSensor = child.GetComponent<Sensor>();
            if (childSensor!=null){
                sensors.Add(child.gameObject);
                //Debug.Log(child.name);
            }
        }
        notifications = new Queue<Notification>();
        newSignals = new Queue<Signal>();
	}

	// Update is called once per frame
	void Update()
	{
        if(newSignals.Count!=0){
            //Debug.Log("Adding new signal");
            addSignal(newSignals.Dequeue());
        }
        SendSignals();
	}

    public void addSignal(Signal signal){
        //Sensor[] validSensors;
        foreach(GameObject sensor in sensors){
            //Check if sensor can detect modality
            if (!sensor.GetComponent<Sensor>().detectsModality(signal.modality)){
                Debug.Log("Sensor "+sensor.name+" cant detect modality");
                continue;
            }
            //Check range
            float distance = (signal.position-sensor.transform.position).magnitude;
            if (signal.modality.maximumRange<distance){
                Debug.Log("distance "+distance+" is greater than range "+signal.modality.maximumRange);
                continue;
            }
            //Check threshold for intensity
            float intensity = signal.strenght * Mathf.Pow(signal.modality.attenuation,distance);
            if (intensity<sensor.GetComponent<Sensor>().threshold){
                Debug.Log("intensity "+intensity+" is less than threshold "+sensor.GetComponent<Sensor>().threshold);
                continue;
            }
            //Additional checks
            if(!signal.modality.extraChecks()){
                Debug.Log("Extra checks didnt pass");
                continue;
            }
            //Time for notification will depend on signal modality (smell is later)
            //Debug.Log("Sensor will be notified in "+distance*signal.modality.inverseTransmissionSpeed+" seconds");
            DateTime time = System.DateTime.Now.AddSeconds(distance*signal.modality.inverseTransmissionSpeed);
            //Create notification record
            Notification notification = new Notification(time, sensor.name, signal);
            notifications.Enqueue(notification);

        }
    }

    public void SendSignals(){
        DateTime currentTime=DateTime.Now;
        while (notifications.Count!=0){
            Notification notification = notifications.Peek();
            //Check if notification is due
            if (DateTime.Compare(notification.time,currentTime)<0){ //notification.time<currentTime
                //
                GameObject sensor = GameObject.Find(notification.sensorName);
                //Debug.Log("Notification is due for "+notification.sensorName);
                sensor.GetComponent<Sensor>().Notify(notification.signal);
                notifications.Dequeue();

            }else{
                break;
            }

        }
    }

}