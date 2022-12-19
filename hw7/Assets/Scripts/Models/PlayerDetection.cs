using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        //当玩家与巡逻兵相撞
        if (other.gameObject.tag == "Player")
        {
            //玩家死亡
            other.gameObject.GetComponent<Animator>().SetBool("live",false);
            //Enemy发动攻击
            this.GetComponent<Animator>().SetTrigger("attack_tri");
            //游戏结束
            Singleton<GameEventManager>.Instance.playerGameOver();
        }
    }
}
