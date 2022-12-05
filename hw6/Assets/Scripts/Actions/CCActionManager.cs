using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    public RoundController sceneController;
    public CCFlyAction fly;


    protected new void Start()
    {
        sceneController = (RoundController)Director.getInstance().currentSceneController;
        sceneController.actionManager = this;
    }

    // Update is called once per frame
    // protected new void Update(){}

    public void SSActionEvent(
        SSAction source,
        SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null) {
    }

    public void MoveArrow(GameObject arrow, Vector3 wind, Vector3 force) {
        fly = CCFlyAction.GetCCFlyAction(wind,force);
        RunAction(arrow, fly, this);
    }

}

