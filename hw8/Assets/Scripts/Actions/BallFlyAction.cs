using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFlyAction : SSAction
{
    private Vector3 force = new Vector3(0,80f,30f);
    private Vector3 gravity = new Vector3(0,-0.3f,0);
    // public float speed;
    public static BallFlyAction GetBallFlyAction(){
        BallFlyAction action = ScriptableObject.CreateInstance<BallFlyAction>();
        return action;
    }

    public override void Start(){
        
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().AddForce(force,ForceMode.Impulse);
    }

    public override void Update()
    {   
        if (this.gameObject.tag == "Finish"){
            this.destroy = true;
        }  
        gameObject.GetComponent<Rigidbody>().AddForce(gravity,ForceMode.Impulse);
    }

}
