using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        EnemyData parent = this.gameObject.transform.parent.GetComponent<EnemyData>();
    	//玩家进入Enemy追捕范围，开始追捕
        if (collider.gameObject.tag == "Player")
        {
            //启动追捕模式
            parent.isFollow = true;
            //将追捕对象设置为玩家
            parent.player = collider.gameObject;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        EnemyData parent = this.gameObject.transform.parent.GetComponent<EnemyData>();
        //玩家跑出Enemy追捕范围/玩家跑出Enemy管控范围，结束追捕
        if (collider.gameObject.tag == "Player" || parent.AreaID != parent.CurID)
        {
            //关闭追捕模式
            parent.isFollow = false;
            //将追捕对象设置为空
            parent.player = null;
        }
    }
}
