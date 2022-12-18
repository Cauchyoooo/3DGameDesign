using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public PropFactory factory;                              // Enemy和Baby工厂
    public ScoreController scoreController;                         // 记分员
    public EnemyActionManager manager;                      // 运动管理器
    public myUserGUI userGUI;
    public int CurID = -1;                                   // 当前玩家所于检测区域的序号
    public GameObject player;                                // 玩家
    public Camera cam;                                       // 主相机
    public float moveSpeed = 5;                              // 玩家移动速度
    public float rotateSpeed = 250f;                         // 玩家旋转速度
    private List<GameObject> enemies;                        // 场景中Enemy列表
    private List<GameObject> babies;                         // 场景Baby列表
    private bool isGameOver = false;                         // 游戏是否结束

    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].gameObject.GetComponent<EnemyData>().CurID = CurID;
        }
        //金矿收集完毕
        if(scoreController.getBabyNum() == 0)
        {
            GameOver();
        }
    }
    void Start()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        gameObject.AddComponent<PropFactory>();
        factory = Singleton<PropFactory>.Instance;
        manager = gameObject.AddComponent<EnemyActionManager>() as EnemyActionManager;
        scoreController = gameObject.AddComponent<ScoreController>() as ScoreController;
        userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        LoadResource();
        cam.GetComponent<CameraFlow>().target = player;
        // scoreController = Singleton<ScoreController>.Instance;
        // userGUI = gameObject.AddComponent<myUserGUI>() as myUserGUI;
        
    }

    public void LoadResource()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Map"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        babies = factory.getBabies();
        enemies = factory.getEnemies();

        //所有侦察兵移动
        for (int i = 0; i < enemies.Count; i++)
        {
            manager.Walk(enemies[i]);
        }
    }

    // public void cleanRig(){
    //     Rigidbody rig = player.GetComponent<Rigidbody>();
    //     rig.
    // }

    //玩家移动
    public void movePlayer(float tranX, float tranZ, bool isShift)
    {
        if(!isGameOver)
        {
            if (tranX != 0 || tranZ != 0)
            {
                player.GetComponent<Animator>().SetBool("isWalk", true);
                if (isShift){
                    player.GetComponent<Animator>().SetBool("isRun", true);
                    moveSpeed = 10;
                }
                else{
                    player.GetComponent<Animator>().SetBool("isRun", false);
                    moveSpeed = 5;
                }
            }
            else
            {
                player.GetComponent<Animator>().SetBool("isWalk", false);
                return;
            }
            //移动和旋转
            player.transform.Translate(0, 0, tranZ * moveSpeed * Time.deltaTime);
            player.transform.Rotate(0, tranX * rotateSpeed * Time.deltaTime, 0);

            //防止碰撞带来的移动
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }     
        }
    }

    public int getScore()
    {
        return scoreController.getScore();
    }

    public int getBabyNum()
    {
        return scoreController.getBabyNum();
    }
    public bool getGameOver()
    {
        return isGameOver;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/startScene");
    }

    void OnEnable()
    {
        GameEventManager.scoreChange += addScore;
        GameEventManager.gameOverChange += GameOver;
        GameEventManager.babyChange += reduceBabyNum;
    }
    void OnDisable()
    {
        GameEventManager.scoreChange -= addScore;
        GameEventManager.gameOverChange -= GameOver;
        GameEventManager.babyChange -= reduceBabyNum;
    }

    void reduceBabyNum()
    {
        scoreController.reduceBaby();
    }
    void addScore()
    {
        scoreController.addScore();
    }
    void GameOver()
    {
        isGameOver = true;
        factory.stopEnemies();
        manager.DestroyAllAction();
    }
}