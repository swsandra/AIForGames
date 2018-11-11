using UnityEngine;
using System.Collections;

public class Projectile : GeneralBehaviour
{
    bool set = false;
    Vector3 firePos, direction;
    float speed, timeElapsed;
    
    // Use this for initialization
    new void Start()
    {

    }

    // Update is called once per frame
    new void Update()
    {
        
    }

    public void Set(Vector3 firePos, Vector3 direction, float speed, bool set){
        
        this.firePos = firePos;
        this.direction = direction.normalized;
        this.speed=speed;
        transform.position=firePos;
        this.set=set;

    }

    public static Vector3 GetFireDirection(Vector3 startPos, Vector3 endPos, float speed){

        Vector3 direction = Vector3.zero;
        Vector3 delta = endPos-startPos;
        float a = Vector3.Dot(Physics.gravity, Physics.gravity);
        float b = -4 * (Vector3.Dot(Physics.gravity, delta) + speed * speed);
        float c = 4 * Vector3.Dot(delta,delta);
        if (4*a*c > b*b){
            return direction;
        }
        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b*b-4*a*c))/(2*a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b*b-4*a*c))/(2*a));

        float time;
        if (time0 < 0f){
            if (time1<0f){
                return direction;
            }
            time=time1;
        }
        else{
            if (time1<0f){
                time=time0;
            }else{
                time=Mathf.Min(time0, time1);
            }
        }
        direction=2*delta - Physics.gravity * (time*time);
        direction = direction / (2*speed*time);
        return direction;

    }

}
