using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myUserGUI : MonoBehaviour
{
    public int mode;
    public int score;
    public int round;
    public string gameMessage;
    private IUserAction action;
    public GUIStyle titleStyle, textStyle;
    private int menu_w = Screen.width/5, menu_h = Screen.height/5;

    void Start()
    {
        mode = 0;
        gameMessage = "";
        action = Director.getInstance().currentSceneController as IUserAction;
    
        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.normal.background = null;
        titleStyle.fontSize = 50;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.normal.background = null;
        textStyle.fontSize = 20;
        textStyle.alignment = TextAnchor.MiddleCenter;

    }

    void Update(){}

    void OnGUI()
    {
        GUI.skin.button.fontSize = 35;
        switch(mode){
            case 0:
                mainMenu();
                break;
            case 1:
                gameStart();
                break;
        }
    }

    void mainMenu()
    {
        GUI.Label(new Rect(Screen.width / 2 - menu_w * 0.5f, Screen.height * 0.1f, menu_w, menu_h), "Hit UFO", titleStyle);
        bool button = GUI.Button(new Rect(Screen.width / 2 - menu_w * 0.5f, Screen.height * 3 / 7, menu_w, menu_h), "Start");
        if (button) {
            mode = 1;
        }
    }

    void gameStart()
    {
        GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-60, 200, 50), gameMessage, titleStyle);
        GUI.Label(new Rect(0, 0, 100, 50), "Score: " + score, textStyle);
        GUI.Label(new Rect(Screen.width-100, 0, 100, 50), "Round: " + round, textStyle);
        if (gameMessage == "Game Over!") {
			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2, 200, 50), "Restart")) {
				action.Restart ();
			}
		}
    }

    public void Reset()
    {
        score = 0;
        round = 1;
        mode = 0;
        gameMessage = "";
    }
    
}
