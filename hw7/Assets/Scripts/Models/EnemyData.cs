using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public int AreaID;                      //  Enemy巡逻区域序号
    public bool isFollow = false;           // 是否跟随玩家
    public int CurID = -1;                  //  当前玩家所在区域序号
    public GameObject player;               //  玩家游戏对象
    public int kind;                        //  巡逻类型 3/4/5边
    public Vector3 startPos;                //  Enemy初始位置   
    public Vector3 lu;                      //  Enemy巡逻区域左上角坐标
    public Vector3 rd;                      //  Enemy巡逻区域右下角坐标
}
