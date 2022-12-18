using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public FirstSceneController sceneController = null;
    public myUserGUI userGUI;
    public int score = 0;
    public int babyNum = 7;
    void Start()
    {
        sceneController = (FirstSceneController)Director.getInstance().currentSceneController;
        sceneController.scoreController = this;
        userGUI = this.gameObject.GetComponent<myUserGUI>();
    }

    public int getScore()
    {
        return score;
    }
    public void addScore()
    {
        score++;
    }
    public int getBabyNum()
    {
        return babyNum;
    }
    public void reduceBaby()
    {
        babyNum--;
    }

    public void Reset(){
        score = 0;
    }
}
