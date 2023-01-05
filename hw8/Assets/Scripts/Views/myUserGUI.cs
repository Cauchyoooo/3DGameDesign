using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myUserGUI : MonoBehaviour
{
    private IUserAction action;
    GUIStyle titleStyle = new GUIStyle();
    GUIStyle textStyle = new GUIStyle();
    void Start(){
        action = Director.getInstance().currentSceneController as IUserAction;

        titleStyle.normal.textColor = new Color(0.9f, 0.65f, 0.99f, 1);
        titleStyle.normal.background = null;
        titleStyle.fontSize = 30;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        textStyle.normal.textColor = Color.white;
        textStyle.normal.background = null;
        textStyle.fontSize = 20;
    }
    void Update(){
        if(!action.IsBallNull()){
            if (Input.GetKeyDown(KeyCode.J)) action.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Space)) action.Init();
        if (Input.GetKeyDown(KeyCode.Z)) action.ChangeView('z');
        if (Input.GetKeyDown(KeyCode.X)) action.ChangeView('x');
        if (Input.GetKeyDown(KeyCode.C)) action.ChangeView('c');
    }

    void OnGUI() {
        GUI.Label(new Rect(Screen.width-200, 5, 200, 50), "粒子效果展示", titleStyle);
        GUI.Label(new Rect(12, 12, 200, 50), "视角切换：z, x, c", textStyle);   
        GUI.Label(new Rect(12, 42, 200, 50), "取物：space", textStyle);
        GUI.Label(new Rect(12, 72, 200, 50), "发射：j", textStyle);    
    }
}