using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myUserGUI : MonoBehaviour
{
    private IUserAction action;
	public string gameMessage ;
    public int time;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start(){
		time = 60;
        action = Director.getInstance ().currentSceneController as IUserAction;

		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;
    }

    void OnGUI() {
        GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), gameMessage, style);
        GUI.Label(new Rect(50, 0,100,50), "Time: " + time, style);
		if (gameMessage == "Game Over!" || gameMessage == "You Win!") {
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				action.Restart ();
			}
		}
	}
}