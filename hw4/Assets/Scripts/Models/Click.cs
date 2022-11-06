using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour 
{
    IUserAction userAction;
    myCharacterController characterController;

    public void setController(myCharacterController cC){
        characterController = cC;
    }

    void Start(){
        userAction = Director.getInstance().currentSceneController as IUserAction;
    }

    void OnMouseDown(){
        if(gameObject.tag == "Boat")
            userAction.moveBoat();
        else if(gameObject.tag == "Priest" || gameObject.tag == "Devil")
            userAction.ClickCharacter(characterController);
    }
}
