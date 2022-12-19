using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour
{
    public int AreaID = 0;
    FirstSceneController sceneController;

    void Start()
    {
        sceneController = Director.getInstance().currentSceneController as FirstSceneController;
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            sceneController.CurID = AreaID;
        }
    }
}
