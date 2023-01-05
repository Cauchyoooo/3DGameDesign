using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActionManager : SSActionManager, ISSActionCallback 
{
    public SceneController sceneController;
    public BallFlyAction fly;
    public BallFactory factory;

    protected new void Start()
    {
        sceneController = (SceneController)Director.getInstance().currentSceneController;
        sceneController.actionManager = this;
        factory = Singleton<BallFactory>.Instance;
    }

    // Update is called once per frame
    // protected new void Update(){}

    public void MoveBall(GameObject ball) {
        fly = BallFlyAction.GetBallFlyAction();
        RunAction(ball, fly, this);
    }
}
