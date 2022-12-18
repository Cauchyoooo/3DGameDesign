using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionManager : SSActionManager
{
    private EnemyWalkAction walk;

    public void Walk(GameObject enemy)
    {
        EnemyData enemyData = enemy.gameObject.GetComponent<EnemyData>();
        walk = EnemyWalkAction.GetSSAction(enemyData.kind,enemy.transform.position,enemyData.lu,enemyData.rd);
        this.RunAction(enemy, walk, this);
    }
    //停止所有动作
    public void DestroyAllAction()
    {
        DestroyAll();
    }
}
