using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour, ISceneController, IUserAction
{
    ArrowFactory factory;
    public CCActionManager actionManager;
    public ScoreController scoreController;
    public myUserGUI userGUI;

    public GameObject target;
    public GameObject arrow;
    public bool isStart = false;
    public string windTag = "";

    private float wind_directX;     
	private float wind_directY; 

    void Start()
    {   
        Director director = Director.getInstance();
        director.currentSceneController = this;
        gameObject.AddComponent<ArrowFactory>();
        factory = Singleton<ArrowFactory>.Instance;
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        scoreController = gameObject.AddComponent<ScoreController>() as ScoreController;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        LoadResource();
    }
    
    public void LoadResource()
    {
        target = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/target"));
        GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/wall"));

    }

    void Update()
    {
        if(isStart) {
            factory.FreeArrow();
        }
    }

    public void Init(){
        if(arrow == null){
            wind_directX = Random.Range(-4, 4);
			wind_directY = Random.Range(-4, 4);
            CreateWind();
            arrow = factory.GetArrow();
        }
    }

    public void moveBow(Vector3 mousePos){
        if(!isStart)
            return;
        arrow.transform.LookAt(mousePos * 30);
    }

    public void shootArrow(Vector3 force){
        if(isStart)
		{
			Vector3 wind = new Vector3(wind_directX, wind_directY, 0);
			actionManager.MoveArrow(arrow, wind, force * 10);
			arrow = null;
		}
    }
    public bool GetStart(){
        return isStart;
    }

    public string GetWind()
    {
        return windTag;
    }


    public void Begin()
    {
        isStart = true;
        // Cursor.visible = false;
    }
    
    public void Restart(){
        // round = 1;
        // currDisk = 0;
        // timer = 1.0F;
        // scoreController.Reset();
        // userGUI.Reset();
    }

    public int GetScore()
    {
        return scoreController.score;
    }

    public void CreateWind(){
        string Horizontal = "", Vertical = "", level = "";
        if (wind_directX > 0) {
            Horizontal = "西";
        } else if (wind_directX <= 0) {
            Horizontal = "东";
        }
        if (wind_directY > 0) {
            Vertical = "南";
        } else if (wind_directY <= 0) {
            Vertical = "北";
        }
        if ((wind_directX + wind_directY) / 2 > -1 && (wind_directX + wind_directY) / 2 < 1)
		{
			level = "1 级";
		}
		else if ((wind_directX + wind_directY) / 2 > -2 && (wind_directX + wind_directY) / 2 < 2)
		{
			level = "2 级";
		}
		else if ((wind_directX + wind_directY) / 2 > -3 && (wind_directX + wind_directY) / 2 < 3)
		{
			level = "3 级";
		}
		else if ((wind_directX + wind_directY) / 2 > -5 && (wind_directX + wind_directY) / 2 < 5)
		{
			level = "4 级";
		}
        windTag = Horizontal + Vertical + "风" + " " + level;
    
    }

    public bool isArrowNull(){
        return (arrow != null);
    }

    
}
