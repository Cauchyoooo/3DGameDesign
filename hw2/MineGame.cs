using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGame : MonoBehaviour
{
    // Data
    private int[,] control_arr = new int[16,20]; //用于记录雷和数字 -1是雷 0是空 其他是周围的雷数
    private int[,] show_arr = new int[16,20]; //用于记录格子可见性 0是可见 1是不可见
    private int[,] mark_arr = new int[16,20]; //用于玩家标记雷 标记雷为1 此时按钮不可点击
    

    private int mine_num = 60; //用于记录剩余雷数
    private int wrong = 0; //用于记录标错的雷数
    private int time = 0; //用于记录用时
    private float tmptime = 0; //用于记录上一帧时间
    private int state = 0; //0为挖矿模式，1为标记模式 输了为-1，赢了为-2

    // Controls Style
    GUIStyle smileStyle = new GUIStyle();
    GUIStyle mineStyle = new GUIStyle();
    GUIStyle[] numStyle = new GUIStyle[8]; //记录数字1-8的格式
    GUIStyle state0 = new GUIStyle();
    GUIStyle state1 = new GUIStyle();
    GUIStyle worl = new GUIStyle();
    GUIStyle txt = new GUIStyle();
    
    

    // System Handlers
    void Start () {
        // 对图标格式的初始化
        smileStyle.fontSize=50;
        smileStyle.normal.textColor=Color.yellow;
        smileStyle.alignment=TextAnchor.MiddleCenter;

        mineStyle.fontSize=20;
        mineStyle.normal.textColor=Color.red;
        mineStyle.alignment=TextAnchor.MiddleCenter;

        state0.fontSize=20;
        state0.normal.textColor=Color.red;
        state0.alignment=TextAnchor.MiddleCenter;

        state1.fontSize=20;
        state1.normal.textColor=Color.black;
        state1.alignment=TextAnchor.MiddleCenter;

        worl.fontSize = 50;
        worl.normal.textColor = Color.red; //win=red / lose=blue
        worl.alignment=TextAnchor.MiddleCenter;

        txt.fontSize = 30;
        txt.normal.textColor=Color.white;
        txt.alignment=TextAnchor.MiddleCenter;
         
        for(int i=0;i<8;i++){
            numStyle[i] = new GUIStyle();
            numStyle[i].fontSize = 20;
            numStyle[i].alignment = TextAnchor.MiddleCenter;
        }
        numStyle[0].normal.textColor=Color.blue; //数字1红色
        numStyle[1].normal.textColor=Color.green; //数字2蓝色
        numStyle[2].normal.textColor=Color.red; //数字3蓝色
        numStyle[3].normal.textColor=new Color(1.00f,0.84f,0.00f,1.00f); //数字4蓝色
        numStyle[4].normal.textColor=new Color(0.63f,0.13f,0.94f,1.00f); //数字5紫色
        numStyle[5].normal.textColor=new Color(1.00f,0.38f,0.00f,1.00f); //数字6橙色
        numStyle[6].normal.textColor=new Color(1.00f,0.75f,0.80f,1.00f); //数字7粉色
        numStyle[7].normal.textColor=Color.black; //数字8黑色

        Init();
    }

    void OnGUI() {
        GUI.Box(new Rect(720,180,480,720),"");
            // 生成smile按钮 for restart
            if(GUI.Button(new Rect(935,220,50,50), "☺",smileStyle)){
                Debug.Log("Push Smile");
                if(state>=0)
                    Init();             
            }
            // 生成挖雷按钮
            if(GUI.Button(new Rect(1000,200,50,50), "挖",state0)){
                state = 0;
                state0.normal.textColor=Color.red;
                state1.normal.textColor=Color.black;

            }

            // 生成标记按钮
            if(GUI.Button(new Rect(1000,240,50,50), "标",state1)){
                state = 1;
                state0.normal.textColor=Color.black;
                state1.normal.textColor=Color.red;

            }
            //显示剩余雷数
            GUI.Button(new Rect(736,220,160,60), "剩余雷数:"+mine_num.ToString());

            //显示时间
            GUI.Button(new Rect(1050,220,130,60), "用时(s):"+time.ToString());

            // 每个大小28*28
            // 生成可点击扫雷按钮
            for(int i=0;i<16;i++){
                for(int j=0;j<20;j++){
                    if(show_arr[i,j]==1)    continue;
                    // 标记的格子可取消标记
                    if(mark_arr[i,j]==1){
                        if(GUI.Button(new Rect(736+i*28,300+j*28,28,28),"✯")){
                            if(state == 1){
                                mark_arr[i,j] = 0;
                                mine_num++;
                                if (control_arr[i,j]<0)
                                    wrong--;
                            }
                        }
                        continue;
                    }

                    if(GUI.Button(new Rect(736+i*28,300+j*28,28,28),"")){
                        // 按下按钮的行为
                        if(state == 1){
                            mark_arr[i,j] = 1;
                            mine_num--;
                            if (control_arr[i,j]>-1)
                                wrong++;
                            if (mine_num==0)
                            {
                                if(wrong>0){
                                    showAllMine();
                                    state = -1; //lose
                                }
                                else{    
                                    state = -2; //win
                                }
                                
                            }
                        }
                        else{
                            if(mark_arr[i,j]==0){
                                show_arr[i,j]=1;
                                if(control_arr[i,j]==0)
                                    showEmpty(i,j);
                                if(control_arr[i,j]<0){
                                    showAllMine();
                                    state = -1; //lose
                                }
                            }
                        }
                    }
                }
            }
            // 显示数字和雷    
            for(int i=0;i<16;i++){
                for(int j=0;j<20;j++){
                    if(show_arr[i,j]==0)    continue; //
                    int ctlnum = control_arr[i,j];
                    if(ctlnum==0){
                        GUI.Label(new Rect(736+i*28,300+j*28,28,28)," ");
                    }
                    else if(ctlnum>0){
                        GUI.Label(new Rect(736+i*28,300+j*28,28,28),ctlnum.ToString(),numStyle[ctlnum-1]);
                    }
                    else{
                        GUI.Label(new Rect(736+i*28,300+j*28,28,28),"✵",mineStyle);
                    }
                    
                }
            }

        if(state<0){
            GUI.Box(new Rect(810,420,300,240),"");
            if(GUI.Button(new Rect(910,630,100,20), "Restart"))
                Init();
            if(state == -1){
                worl.normal.textColor =  Color.blue;
                GUI.Label(new Rect(910,440,100,50), "LOSE", worl);
            }
            else{
                worl.normal.textColor =  Color.red;
                GUI.Label(new Rect(910,440,100,50), "WIN", worl);
            }
            
            int endnum = 60 - mine_num - wrong;
            GUI.Label(new Rect(910,510,100,50), "扫雷数："+endnum.ToString(), txt);
            GUI.Label(new Rect(910,560,100,50), "总用时："+time.ToString(), txt);

            tmptime = Mathf.Floor(Time.fixedTime);
        }
        else{
            //计时
            if(Mathf.Floor(Time.fixedTime)-tmptime==1){
                tmptime = Mathf.Floor(Time.fixedTime);
                time++;
            }
    
        }

    }

    void Init() {
        // 时间，雷数等参数初始化
        time = 0;
        mine_num = 60;
        wrong = 0;
        state = 0;
        
        for(int i=0;i<16;i++){
            for(int j=0;j<20;j++){
                // 格子可见性初始化
                show_arr[i,j] = 0;
            }
        }

        // 雷区初始化
        clearMine();
        InitMine();
    }


    void InitMine(){
        //初始化数字和雷区
        int num=0;
        while (num<60)
        {
            int x=Random.Range(0,16);
            int y=Random.Range(0,20);
            if(control_arr[x,y]==0){
                control_arr[x,y] = -1;
                num++;
            }
        }

        for(int i=0;i<16;i++){
            for(int j=0;j<20;j++){
                if(control_arr[i,j]>-1){
                    //左边
                    if(i>0 && control_arr[i-1,j]==-1)
                        control_arr[i,j]++;
                    //右边
                    if(i<15 && control_arr[i+1,j]==-1)
                        control_arr[i,j]++;
                    //上方
                    if(j>0 && control_arr[i,j-1]==-1)
                        control_arr[i,j]++;
                    //下方
                    if(j<19 && control_arr[i,j+1]==-1)
                        control_arr[i,j]++;
                    //左上角
                    if(i>0 && j>0 && control_arr[i-1,j-1]==-1)
                        control_arr[i,j]++;
                    //右下角
                    if(i<15 && j<19 && control_arr[i+1,j+1]==-1)
                        control_arr[i,j]++;
                    //右上角
                    if(i<15 && j>0 && control_arr[i+1,j-1]==-1)
                        control_arr[i,j]++;
                    //左下角
                    if(i>0 && j<19 && control_arr[i-1,j+1]==-1)
                        control_arr[i,j]++;
                }
            }
        }
    }

    void clearMine(){
        for(int i=0;i<16;i++){
            for(int j=0;j<20;j++){
                control_arr[i,j]=0;
                mark_arr[i,j]=0;  
            }
        }
    }

    void showAllMine(){
        for(int i=0;i<16;i++){
            for(int j=0;j<20;j++){
                if(control_arr[i,j]==-1){
                    show_arr[i,j]=1;
                }  
            }
        }
    }

    // 递归显示无雷区域
    void showEmpty(int i,int j){
        // 遇到被标记的非雷 直接翻开
        if(mark_arr[i,j]==1){
            mark_arr[i,j]=0;
            mine_num++;
        }            
        show_arr[i,j]=1;
        if(control_arr[i,j]>0)
            return;
   
        if(i>0 && show_arr[i-1,j]==0)
            showEmpty(i-1,j);
        if(i<15 && show_arr[i+1,j]==0)
            showEmpty(i+1,j);
        if(j>0 && show_arr[i,j-1]==0)
            showEmpty(i,j-1);
        if(j<19 && show_arr[i,j+1]==0)
            showEmpty(i,j+1);
        if(i>0 && j>0 && show_arr[i-1,j-1]==0)
            showEmpty(i-1,j-1);
        if(i<15 && j<19 && show_arr[i+1,j+1]==0)
            showEmpty(i+1,j+1);
        if(i<15 && j>0 && show_arr[i+1,j-1]==0)
            showEmpty(i+1,j-1);
        if(i>0 && j<19 && show_arr[i-1,j+1]==0)
            showEmpty(i-1,j+1);
        
    }
}