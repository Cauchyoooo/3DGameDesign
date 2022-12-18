using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    // public float speed;
    // public static CCFlyAction GetCCFlyAction(float s){
    //     CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
    //     action.speed = s;
    //     return action;
    // }

    // public override void Start(){}

    // public override void Update()
    // {
    //     //飞碟已经被"销毁"
    //     if (this.transform.gameObject.activeSelf == false) { 
    //         Debug.Log("Hit Destroy");
    //         this.destroy = true;
    //         this.callback.SSActionEvent(this);
    //         return;
    //     }
    //     if(this.transform.position.z <= -20){
    //         Debug.Log("Out Destroy");
    //         this.destroy = true;
    //         this.callback.SSActionEvent(this);
    //         return;
    //     }

    //     transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    //     transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 30 * Time.deltaTime));
    // }
}
