using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    readonly Vector3 waterPos = new Vector3(0,0.5F,0);
    public CCActionManager actionManager;
    public JudgeController judgeController;
    public CoastController startCoast;
    public CoastController endCoast;
    public BoatController boat;
    private myCharacterController[] characters;
    myUserGUI userGUI;
    public bool isRunning;
    public float time;

    public void JudgeCallback(bool _isRunning, string message)
    {
        userGUI.gameMessage = message;
        userGUI.time = (int)time;
        this.isRunning = _isRunning;
    }

    void Start ()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        judgeController = gameObject.AddComponent<JudgeController>() as JudgeController;
        characters = new myCharacterController[6];
        isRunning = true;
        time = 60;
        LoadResources();
    }

    public void LoadResources(){
        GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), waterPos, Quaternion.identity, null) as GameObject;
        water.name = "water";
        water.tag = "Water";

        startCoast = new CoastController("Start");
        endCoast = new CoastController("End");
        boat = new BoatController();

        for(int i=0;i<3;i++){
            myCharacterController priest = new myCharacterController("Priest");
            priest.setName("Priest "+i);
            priest.setTag("Priest");
            priest.setPosition(startCoast.getEmptyPosition());
            priest.getOnCoast(startCoast);
            startCoast.getOnCoast(priest);
            characters[i] = priest;
        }

        for(int i=0;i<3;i++){
            myCharacterController devil = new myCharacterController("Devil");
            devil.setName("Devil "+i);
            devil.setTag("Devil");
            devil.setPosition(startCoast.getEmptyPosition());
            devil.getOnCoast(startCoast);
            startCoast.getOnCoast(devil);
            characters[i+3] = devil;
        }
    }

    public void moveBoat(){
        if(isRunning == false || actionManager.IsMoving())
            return;
        if(boat.isEmpty())
            return;
        string direct = boat.getDirection();
        Vector3 destination = boat.getPosition();        
        if (direct == "End") {
			boat.setDirection("Start");
		} 
		else {
			boat.setDirection("End");
		}
        actionManager.MoveBoat(boat.getGameobj(), destination, 20);
    }

    public void ClickCharacter(myCharacterController cC){
        if(isRunning == false || actionManager.IsMoving())
            return;
        if(cC.isOnBoat()){
            CoastController coast;
            if(boat.getDirection()=="Start")
                coast = startCoast;
            else
                coast = endCoast;

            boat.getOffBoat(cC.getName());
            Vector3 destination = coast.getEmptyPosition();
            Vector3 middle = destination;
            if (destination.y < cC.GetGameObject().transform.position.y) {
			    middle.y = cC.GetGameObject().transform.position.y;
		    } 
		    else {
			    middle.x = cC.GetGameObject().transform.position.x;
		    }
            // cC.moveTo(coast.getEmptyPosition());
            actionManager.MoveRole(cC.GetGameObject(), middle, destination, 20);
            cC.getOnCoast(coast);
            coast.getOnCoast(cC);
        }
        else{
            CoastController coast = cC.getCoastController();
            if(boat.getEmptyIndex () == -1 || coast.getDirection() != boat.getDirection())
                return;
            coast.getOffCoast(cC.getName());
            Vector3 destination = boat.getEmptyPosition();
            Vector3 middle = destination;
            if (destination.y < cC.GetGameObject().transform.position.y) {
			    middle.y = cC.GetGameObject().transform.position.y;
		    } 
		    else {
			    middle.x = cC.GetGameObject().transform.position.x;
		    }
            actionManager.MoveRole(cC.GetGameObject(), middle, destination, 20);
            // cC.moveTo(boat.getEmptyPosition());
            cC.getOnBoat(boat);
            boat.getOnBoat(cC);
        }

        // userGUI.status = checkGameOver();
    }

    public void Restart(){
        //boat 移动归位
        if(boat.getDirection()=="End"){
            Vector3 destination = boat.getPosition();
            actionManager.MoveBoat(boat.getGameobj(), destination, 20);
        }
        boat.reset();
        startCoast.reset();
        endCoast.reset();
        for(int i=0;i<characters.Length;i++)
            characters[i].reset();
        isRunning = true;
        time = 60;
    }

    void Update() {
        if (isRunning)
        {
            time -= Time.deltaTime;
            userGUI.time = (int)time;
        }
    }
}
