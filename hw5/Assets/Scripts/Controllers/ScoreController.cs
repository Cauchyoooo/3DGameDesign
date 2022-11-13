using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public int score;
    public RoundController roundController;
    public myUserGUI userGUI;
    void Start()
    {
        roundController = (RoundController)Director.getInstance().currentSceneController;
        roundController.scoreController = this;
        userGUI = this.gameObject.GetComponent<myUserGUI>();
    }

    public void Record(GameObject disk){
        score += disk.GetComponent<Test>().diskItem.attributes.score;
        userGUI.score = score;
    }

    public void Reset(){
        score = 0;
    }
}
