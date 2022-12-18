using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback 
{
    // public RoundController sceneController;
    // public CCFlyAction action;
    // public DiskFactory factory;

    // protected new void Start()
    // {
    //     sceneController = (RoundController)Director.getInstance().currentSceneController;
    //     sceneController.actionManager = this;
    //     factory = Singleton<DiskFactory>.Instance;
    // }

    // // Update is called once per frame
    // // protected new void Update(){}

    // public void SSActionEvent(
    //     SSAction source,
    //     SSActionEventType events = SSActionEventType.Completed,
    //     int intParam = 0,
    //     string strParam = null,
    //     Object objectParam = null) {
    //         factory.freeDisk(source.transform.gameObject);
    // }

    // public void MoveDisk(GameObject disk) {
    //     action = CCFlyAction.GetCCFlyAction((float)disk.GetComponent<Test>().diskItem.attributes.speed);
    //     RunAction(disk, action, this);
    // }
}
