using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    public Vector3 force;
    public Vector3 wind;
    private Vector3 rot = new Vector3(0,0,0);
    private bool isTrem = false;

    public static CCFlyAction GetCCFlyAction(Vector3 _wind, Vector3 _force){
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        action.force = _force;
        action.wind = _wind;
        action.rot.z = _wind.x;
        return action;
    }

    public override void Start(){
        isTrem = false;
        gameObject.transform.LookAt(force);
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().AddForce(force + wind, ForceMode.Impulse);
        gameObject.GetComponent<Rigidbody>().angularVelocity = rot;
    }

    public override void Update()
    {
    }
    // 物理引擎
    public override void FixedUpdate()
    {
        // Debug.Log("enter");
        this.gameObject.GetComponent<Rigidbody>().AddForce(wind, ForceMode.Force);
        if (this.gameObject.tag == "Hit" && isTrem == false){
            this.callback.SSActionEvent(this, this.gameObject);
            isTrem = true;
            // Debug.Log("Enter");
        }
        if (transform.position.y < -10 || transform.position.z > -6.7) {
            this.destroy = true;
            // this.callback.SSActionEvent(this, this.gameObject);
        }
    }

}

