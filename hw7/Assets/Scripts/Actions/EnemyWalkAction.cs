using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkAction : SSAction
{
    private int kind = 4;
    // 运动范围为(areaDown,areaUp),(areaLeft,areaRight)
    private float areaLeft = 0;
    private float areaUp = 0;
    private float areaRight = 0;
    private float areaDown = 0;
    // private enum Dirction { EAST, NORTH, WEST, SOUTH };
    private float x, z;                             //  初始x和z方向坐标
    // private float moveDis;                          //  移动矩形边长
    private float moveSpeed = 1.8f;                 //  移动速度
    private bool isReach = true;                    //  是否到达目的地
    // private Dirction dirction = Dirction.NORTH;     //  移动方向
    private EnemyData enemyData;                    //  Enemy数据
    private List<Vector3> path = new List<Vector3>();   //存储路径点
    private int curTar = 0;     //  当前目标地索引



    private EnemyWalkAction() { }
    public static EnemyWalkAction GetSSAction(int k, Vector3 start, Vector3 lu, Vector3 rd)
    {
        EnemyWalkAction action = CreateInstance<EnemyWalkAction>();
        action.kind = k;
        action.x = start.x;
        action.z = start.z;
        action.areaLeft = lu.x;
        action.areaRight = rd.x;
        action.areaUp = lu.z;
        action.areaDown = rd.z;
        action.initPath();
        // //设定移动矩形的边长
        // action.moveDis = Random.Range(5, 9);
        
        return action;
    }
    Vector3 getPoint(float left, float right, float down, float up){
        Vector3 res= new Vector3(0,0,0);
        if(left==right){    //竖线
            res.x = left;
            res.z = Random.Range(down,up); 
        }
        else if(down==up){
            res.x = Random.Range(left,right);
            res.z = down;
        }
        else{
            res.x = Random.Range(left,right);
            res.z = Random.Range(down,up);
        }
        return res;
    }
    void initPath()
    {
        float midx = (areaLeft+areaRight)/2;
        float midz = (areaUp+areaDown)/2;
        path.Add(getPoint(areaLeft+2f,areaLeft+2f,areaDown+2f,areaUp-2f));
        path.Add(getPoint(areaLeft+2f,areaRight-2f,areaDown+2f,areaDown+2f));
        if(kind==3){
            path.Add(getPoint(areaRight-2f,areaRight-2f,midz,areaUp-2f));
        }
        else if(kind==4){
            path.Add(getPoint(areaRight-2f,areaRight-2f,areaDown+2f,areaUp-2f));
            path.Add(getPoint(areaLeft+2f,areaRight-2f,areaUp-2f,areaUp-2f));
        }
        else if(kind==5){
            float quax = (midx+areaRight)/2;
            float quaz = (midz+areaUp)/2;
            path.Add(getPoint(areaRight-2f,areaRight-2f,areaDown+2f,midz));
            path.Add(new Vector3(quax,0,quaz));
            path.Add(getPoint(areaLeft+2f,midx,areaUp-2f,areaUp-2f));
        }
    }

    void goPatrol()
    {
        if(isReach){
            curTar ++;
            curTar %= kind;
            isReach = false;
        }
        this.transform.LookAt(path[curTar]);
        float distance = Vector3.Distance(transform.position, path[curTar]);
        if (distance > 0.9)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, path[curTar], moveSpeed * Time.deltaTime);
        }
        else{
            isReach = true;
        }
    }
    

    public override void Update()
    {
        //侦察移动
        goPatrol();
        //如果侦察兵需要跟随玩家并且玩家就在侦察兵所在的区域，侦查动作结束
        if (enemyData.isFollow && enemyData.CurID == enemyData.AreaID)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, 0, this.gameObject);
        }
    }
    public override void Start()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isWalk", true);
        enemyData = this.gameObject.GetComponent<EnemyData>();
    }
}
