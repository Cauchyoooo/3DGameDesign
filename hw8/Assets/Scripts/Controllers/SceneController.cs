using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, IUserAction, ISceneController
{
    BallFactory factory;
    public BallActionManager actionManager;
    public myUserGUI userGUI;
    public GameObject ball = null;
    public Camera mainCamera;
    public Camera upCamera;
    public Camera downCamera;

    void Start()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        gameObject.AddComponent<BallFactory>();
        factory = Singleton<BallFactory>.Instance;
        actionManager = gameObject.AddComponent<BallActionManager>() as BallActionManager;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        mainCamera = Camera.main;
        LoadResource();
    }

    public void LoadResource()
    {
        GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Background"));
        upCamera = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UpCamera")).GetComponent<Camera>();
        downCamera = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/DownCamera")).GetComponent<Camera>();
        ChangeView('z');
    }
    
    public void ChangeView(char c)
    {
        if(c == 'z'){
            mainCamera.enabled = true;
            upCamera.enabled = false;
            downCamera.enabled = false;
        }
        else if(c == 'x'){
            mainCamera.enabled = false;
            upCamera.enabled = true;
            downCamera.enabled = false;
        }
        else{
            mainCamera.enabled = false;
            upCamera.enabled = false;
            downCamera.enabled = true;
        }
    }

    public void Init()
    {
        if(ball == null){
            ball = factory.GetBall();
        }
    }

    public void Shoot()
    {
        actionManager.MoveBall(ball);
        ball = null;
    }

    public void Update()
    {
        factory.FreeBall();
    }

    public bool IsBallNull(){
        return (ball == null);
    }
}