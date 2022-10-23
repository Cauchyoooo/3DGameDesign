using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homework.Assets.Scripts;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    readonly Vector3 waterPos = new Vector3(0,0.5F,0);
    public CoastController startCoast;
    public CoastController endCoast;
    public BoatController boat;
    private myCharacterController[] characters;
    myUserGUI userGUI;

    void Start ()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        characters = new myCharacterController[6];
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
        if(boat.isEmpty())
            return;
        boat.Move();
        userGUI.status = checkGameOver();
    }

    public void ClickCharacter(myCharacterController cC){
        if(cC.isOnBoat()){
            CoastController coast;
            if(boat.getDirection()=="Start")
                coast = startCoast;
            else
                coast = endCoast;

            boat.getOffBoat(cC.getName());
            cC.moveTo(coast.getEmptyPosition());
            cC.getOnCoast(coast);
            coast.getOnCoast(cC);
        }
        else{
            CoastController coast = cC.getCoastController();
            if(boat.getEmptyIndex () == -1 || coast.getDirection() != boat.getDirection())
                return;
            coast.getOffCoast(cC.getName());
            cC.moveTo(boat.getEmptyPosition());
            cC.getOnBoat(boat);
            boat.getOnBoat(cC);
        }

        userGUI.status = checkGameOver();
    }

    /// -1->lose 0->unfinish 1->win
    int checkGameOver(){
        int startPriest = 0;
        int endPriest = 0;
        int startDevil = 0;
        int endDevil = 0;

        int[] startCount = startCoast.getNum();
        startPriest += startCount[0];
        startDevil += startCount[1];

        int[] endCount = endCoast.getNum();
        endPriest += endCount[0];
        endDevil += endCount[1];

        if(endDevil+endPriest ==6)
            return 1;
        
        int[] boatCount = boat.getNum();
        if(boat.getDirection() == "End"){
            endPriest += boatCount[0];
            endDevil += boatCount[1];
        }
        else{
            startPriest += boatCount[0];
            startDevil += boatCount[1];
        }

        if(startPriest<startDevil && startPriest>0)
            return -1;
        if(endPriest<endDevil && endPriest>0)
            return -1;
            
        return 0;
        

    }

    public void Restart(){
        boat.reset();
        startCoast.reset();
        endCoast.reset();
        for(int i=0;i<characters.Length;i++)
            characters[i].reset();
    }
}