using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour {
    public FirstController mainController;
    public CoastController startCoast;
    public CoastController endCoast;
    public BoatController boat;


    void Start()
    {
        this.mainController = (FirstController)Director.getInstance().currentSceneController;
        this.startCoast = mainController.startCoast;
        this.endCoast = mainController.endCoast;
        this.boat = mainController.boat;

    }

    void Update()
    {
        if (!mainController.isRunning)
            return;
        if(!boat.isStop())
            return;
        if (mainController.time <= 0)
        {
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
        
        this.gameObject.GetComponent<myUserGUI>().gameMessage = "";

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

        if(endDevil+endPriest ==6){
            mainController.JudgeCallback(false, "You Win!");
            return;
        }

        int[] boatCount = boat.getNum();
        if(boat.getDirection() == "End"){
            endPriest += boatCount[0];
            endDevil += boatCount[1];
        }
        else{
            startPriest += boatCount[0];
            startDevil += boatCount[1];
        }

        if(startPriest<startDevil && startPriest>0){
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
        if(endPriest<endDevil && endPriest>0){
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
    }
}
