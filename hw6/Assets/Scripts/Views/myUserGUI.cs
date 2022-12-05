using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myUserGUI : MonoBehaviour
{
    public bool isStart = false;
    private IUserAction action;
    public GUIStyle titleStyle, textStyle;
    // private int menu_w = Screen.width/5, menu_h = Screen.height/5;

    void Start()
    {

        action = Director.getInstance().currentSceneController as IUserAction;
    
        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.normal.background = null;
        titleStyle.fontSize = 50;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.normal.background = null;
        textStyle.fontSize = 30;
        // textStyle.alignment = TextAnchor.MiddleCenter;

        
        // state = 1;
        // gameMessage = "";
    }

    void Update(){
        if(isStart)
		{
			if (action.isArrowNull()) {
				if (Input.GetMouseButton(0)) {
					Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
					action.moveBow(mousePos);
				}

				if (Input.GetMouseButtonUp(0)) {
					Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
					action.shootArrow(mousePos);
				}
			}
			if (Input.GetKeyDown(KeyCode.Space)) action.Init();
		}
    }

    void OnGUI()
    {
        GUI.skin.button.fontSize = 30;
        if(isStart) {
            GUI.Label(new Rect(0, 5, 200, 50), "分数:", textStyle);
            GUI.Label(new Rect(100, 5, 200, 50), action.GetScore().ToString(), textStyle);
            GUI.Label(new Rect(0, 55, 200, 50), "风向: ", textStyle);
            GUI.Label(new Rect(100, 55, 200, 50), action.GetWind(), textStyle);
            GUI.Label(new Rect(Screen.width/2 - 200, 5, 400, 50), "按空格取箭，鼠标左键发射", textStyle);
        }
        else {
            GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 100, 200, 100), "打靶游戏", titleStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 400, Screen.height / 2 , 200, 50), "游戏开始")) {
                isStart = true;
                action.Begin();
            }
        }
    }


    // public void Reset()
    // {
    //     score = 0;
    //     windTag = "";
    //     // state = 1;
    //     isStart = false;
    //     gameMessage = "";
    // }
    
}

