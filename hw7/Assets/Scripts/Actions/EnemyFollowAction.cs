using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAction : SSAction
{
    private float speed = 2f;                  //跟随玩家的速度
    private GameObject player;                 //玩家
    private EnemyData enemyData;             //侦查兵数据

    private EnemyFollowAction() { }
    public static EnemyFollowAction GetSSAction(GameObject player)
    {
        EnemyFollowAction action = CreateInstance<EnemyFollowAction>();
        action.player = player;
        return action;
    }

    public override void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
        //如果侦察兵没有跟随对象，或者需要跟随的玩家不在侦查兵的区域内
        if (!enemyData.isFollow || enemyData.CurID != enemyData.AreaID)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, 1, this.gameObject);
        }
    }

    public override void Start()
    {
        enemyData = this.gameObject.GetComponent<EnemyData>();
    }
}
