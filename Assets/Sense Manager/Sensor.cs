using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sensor : MonoBehaviour{

	Agent character;

    List<string> modalities;

    float threshold;

	 // Use this for initialization
	void Start()
	{
		character = gameObject.GetComponent<Agent>();
        modalities = new List<string>();
        modalities.Add("sight");
        if (gameObject.name=="Monster_Disgust") {
            modalities.Add("hearing");
            modalities.Add("talk");
            threshold = 3f;
        }
        else if (gameObject.name=="Monster_Anger") {
            modalities.Add("hearing");
            threshold = 3f;
        }
        else if (gameObject.name=="Monster_Sadness"){
            modalities.Add("smell");
            threshold = 4f;
        }
        
	}

	// Update is called once per frame
	void Update()
	{
		
	}

    public bool detectsModality(Modality mod){
        if (modalities.Contains(mod.name)){
            return true;
        }
        return false;
    }    

}