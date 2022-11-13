using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour, ISceneController, IUserAction
{
    int round = 1;
    int currDisk = 0;
    int max_round = 5;
    float timer = 1.0f;
    GameObject disk;
    DiskFactory factory;
    public CCActionManager actionManager;
    public ScoreController scoreController;
    public myUserGUI userGUI;

    void Start()
    {   
        Director director = Director.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResource();
        gameObject.AddComponent<DiskFactory>();
        factory = Singleton<DiskFactory>.Instance;
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        scoreController = gameObject.AddComponent<ScoreController>() as ScoreController;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;

    }
    // No need to loadresource
    public void LoadResource(){}

    void Update()
    {
        if(userGUI.mode == 0)
            return;
        getHit();
        gameOver();
        if(round>max_round)
            return;
        timer -= Time.deltaTime;
        if(timer<=0 ){
            for(int i=0; i<6;i++){
                disk = factory.createDisk(round);
                actionManager.MoveDisk(disk);
            }
            currDisk+=6;
            if(round<=max_round)
                userGUI.round = round;
            timer = (float)(4-round*0.5);
            if(currDisk%24 == 0){
                round++;
                timer = 10.0F;
            }
        }
    }

    public void gameOver()
    {
        if (round > max_round && actionManager.RemainActionCount() == 0)
            userGUI.gameMessage = "Game Over!";
    }

    public void getHit()
    {
        // 按钮设置 名为“Fire1”监听鼠标点击
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Fire pressed");
			Camera ca = Camera.main;
			Ray ray = ca.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.transform.gameObject.name);
                scoreController.Record(hit.transform.gameObject);
                hit.transform.gameObject.SetActive(false);
			}
        }
    }

    public void Restart(){
        round = 1;
        currDisk = 0;
        timer = 1.0F;
        scoreController.Reset();
        userGUI.Reset();
    }
    
}
