using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homework.Assets.Scripts;

public class myUserGUI : MonoBehaviour
{
    private IUserAction action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start(){
        action = Director.getInstance ().currentSceneController as IUserAction;

		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;
    }

    void OnGUI() {
		if (status == -1) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "Gameover!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				status = 0;
				action.Restart ();
			}
		} 
        else if(status == 1) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You win!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				status = 0;
				action.Restart ();
			}
		}
	}
}