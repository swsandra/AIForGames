using UnityEngine;

public class LandingLocation : MonoBehaviour{

    public void OnTriggerEnter(Collider other){
        if (!other.gameObject.CompareTag("Agent")){
            return;
        }

        Agent agent = other.GetComponent<Agent>();
        JumpAI jump = other.GetComponent<JumpAI>();
        
        if (agent==null || jump==null){
            return;
        }

        jump.Isolate(false);
        jump.jumpPoint = null;
    }
    
}