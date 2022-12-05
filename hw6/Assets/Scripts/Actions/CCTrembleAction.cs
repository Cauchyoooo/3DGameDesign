using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTrembleAction : SSAction
{
    float radian = 0;
	float per_radian = 3f;
	float radius = 0.01f;
	Vector3 old_pos;
	public float left_time = 0.5f;

	public override void Start(){
        old_pos = transform.position;
    }
	public static CCTrembleAction GetSSAction(){
		CCTrembleAction tremble = CreateInstance<CCTrembleAction>();
		return tremble;
	}
	public override void Update(){
		left_time -= Time.deltaTime;
		if (left_time <= 0){
			transform.position = old_pos;
			this.destroy = true;
			this.callback.SSActionEvent(this);
		}
		radian += per_radian;
		float dy = Mathf.Cos(radian) * radius; 
		transform.position = old_pos + new Vector3(0, dy, 0);
	}
	public override void FixedUpdate(){}

}