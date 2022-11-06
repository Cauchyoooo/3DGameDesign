using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    
    public Vector3 target; //目的地
    public float speed; //速度
    
    private CCMoveToAction(){}

    //生产函数(工厂模式)
    public static CCMoveToAction GetSSAction(Vector3 target, float speed)
    {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Start(){}
    
    public override void Update()
    {
        //判断是否符合移动条件
        if (this.gameObject == null || this.transform.position == target)
        {

            this.destroy = true;
            this.callback.SSActionEvent(this);
            return;
        }
        //移动
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }    
}
