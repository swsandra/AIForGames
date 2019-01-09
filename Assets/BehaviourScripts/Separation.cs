using UnityEngine;
using System.Collections;

public class Separation : GeneralBehaviour
{
	public Agent[] targets;
	public float threshold=6f, decayCoefficient=10f;

	public bool pushed;

	// Use this for initialization
	new void Start()
	{
		base.Start();
		weight = 1.5f;
		if(gameObject.name=="Monster_Fear"){
			threshold=10f;
		}
		pushed=false;
	}

	// Update is called once per frame
	new void Update()
	{
		/*if(!stop){
			character.SetSteering(GetSteering(), weight, priority);
		}else{
			character.SetSteering(new Steering(), weight, priority);
		} */
	}

	public override Steering GetSteering()
	{

		foreach (Agent aTarget in targets)
		{
			//Vector3 direction = target.transform.position - character.transform.position; //Esto hace algo loco
			Vector3 direction = character.transform.position - aTarget.transform.position;
			float distance = direction.magnitude;
			
			float strength;
			if (distance < threshold)
			{
				strength = Mathf.Min(decayCoefficient/(distance*distance), character.maxAcc);
				direction.Normalize();
				steering.linear += strength * direction;
				if(gameObject.name=="Monster_Fear"){
					steering.linear += 130 * direction;
				}
			}
			else
			{
				steering.linear = Vector3.zero;
			}

		}
		steering.angular = 0f;
		return steering;
	}

}
