using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd)
            actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null)
    {
        EnemyData enemyData = objectParam.gameObject.GetComponent<EnemyData>();
        if(intParam == 0)
        {
            //巡逻兵跟随玩家
            EnemyFollowAction follow = EnemyFollowAction.GetSSAction(enemyData.player);
            this.RunAction(objectParam, follow, this);
        }
        else
        {
            //巡逻兵继续巡逻
            EnemyWalkAction move = EnemyWalkAction.GetSSAction(enemyData.kind, enemyData.startPos, enemyData.lu, enemyData.rd);
            this.RunAction(objectParam, move, this);
            //玩家离开
            Singleton<GameEventManager>.Instance.playerEscape();
        }
    }

	
    public void DestroyAll()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }
}
