using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myUserGUI : MonoBehaviour
{
    private IUserAction action;
    private GUIStyle scoreNumStyle = new GUIStyle();
    private GUIStyle scoreTextStyle = new GUIStyle();
    private GUIStyle scoreShadowStyle = new GUIStyle();
    private GUIStyle titleStyle = new GUIStyle();
    private GUIStyle shadowStyle = new GUIStyle();
    private GUIStyle tipStyle = new GUIStyle();

    void Start ()
    {
        action = Director.getInstance().currentSceneController as IUserAction;
        scoreNumStyle.normal.textColor = new Color(1,0.92f,0.016f,1);
        scoreNumStyle.fontSize = 30;
        scoreTextStyle.normal.textColor = new Color(0, 0, 0, 1);
        scoreTextStyle.fontSize = 30;
        scoreShadowStyle.normal.textColor = new Color(1,1,1,0.4f);
        scoreShadowStyle.fontSize = 30;
        titleStyle.normal.textColor = new Color(0.47F,0.4F,0.93F,1);
        titleStyle.fontSize = 40;
        shadowStyle.normal.textColor = new Color(1,1,1,0.5f);
        shadowStyle.fontSize = 40;
        tipStyle.normal.textColor = new Color(0.54f,0.27f,0.04f,1);
        tipStyle.fontSize = 18;
        
    }

    void Update()
    {
        //获取方向键的偏移量
        float tranX = Input.GetAxis("Horizontal");
        float tranZ = Input.GetAxis("Vertical");
        bool isShift = Input.GetKey(KeyCode.LeftShift);
        //移动玩家
        action.movePlayer(tranX, tranZ, isShift);
    }
    private void OnGUI()
    {
        GUI.skin.button.fontSize = 20;
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", scoreTextStyle);
        GUI.Label(new Rect(9, 4, 200, 50), "分数:", scoreShadowStyle);
        GUI.Label(new Rect(90, 5, 200, 50), action.getScore().ToString(), scoreNumStyle);
        GUI.Label(new Rect(Screen.width - 245, 5, 200, 50), "剩余小狐狸数:", scoreTextStyle);
        GUI.Label(new Rect(Screen.width - 246, 4, 200, 50), "剩余小狐狸数:", scoreShadowStyle);
        GUI.Label(new Rect(Screen.width - 50, 5, 50, 50), action.getBabyNum().ToString(), scoreNumStyle);
        if(action.getGameOver() && action.getBabyNum() != 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 100, 100, 100), "游戏结束", titleStyle);
            GUI.Label(new Rect(Screen.width / 2 - 78, Screen.height / 2 - 98, 100, 100), "游戏结束", shadowStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 , 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        else if(action.getBabyNum() <= 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 100, 100, 100), "恭喜胜利", titleStyle);
            GUI.Label(new Rect(Screen.width / 2 - 78, Screen.height / 2 - 98, 100, 100), "恭喜胜利", shadowStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 , 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }

        GUI.Button(new Rect(Screen.width / 2 - 50 ,10, 100, 50), new GUIContent("提示规则", "按WSAD或方向键移动\n按左Shift键进行加速\n成功躲避猛兽追捕加1分\n找到所有小狐狸即可获胜\n鼠标右键实现视角转动"));
        GUI.Label(new Rect(Screen.width / 2 - 80 ,80, 150, 120), GUI.tooltip, tipStyle);
    }

}
