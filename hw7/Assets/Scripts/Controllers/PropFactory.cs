using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour
{
    private GameObject enemy = null;
    private List<GameObject> usedEnemy = new List<GameObject>();
    private GameObject baby = null;
    private List<GameObject> usedBaby = new List<GameObject>();   
    
    public void InitEnemy(int ID, int kind, float x, float z, float lux, float luz, float rdx, float rdz){
        Vector3 start = new Vector3(x, 0, z);
        enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"+kind));
        enemy.transform.position = start;
        enemy.GetComponent<EnemyData>().AreaID = ID;
        enemy.GetComponent<EnemyData>().kind = kind;
        enemy.GetComponent<EnemyData>().startPos = start;
        enemy.GetComponent<EnemyData>().lu = new Vector3(lux,0,luz);
        enemy.GetComponent<EnemyData>().rd = new Vector3(rdx,0,rdz);
        usedEnemy.Add(enemy); 
    }

    public List<GameObject> getEnemies()
    {
        InitEnemy( 1, 3, -40,  40, -50, 50, -30, 30);
        InitEnemy( 2, 4, -10,  30, -20, 40,  0 , 20);
        InitEnemy( 3, 3,  13,  45,  10, 50,  30, 40);
        InitEnemy( 4, 3,  35,  35,  30, 50,  40, 30);
        InitEnemy( 5, 5, -30,  10, -40, 20, -20, 0 );
        InitEnemy( 6, 3,  25,  20,  20, 30,  30, 10);
        InitEnemy( 7, 4,  35,  10,  30, 20,  40, 0);
        InitEnemy( 8, 3,  45, -3 ,  40, 10,  50,-10);
        InitEnemy( 9, 3, -15, -5 , -20, 10, -10,-10);
        InitEnemy(10, 4, -40, -15, -50,-10, -30,-20);
        InitEnemy(11, 5, -30, -30, -40,-20, -20,-40);
        InitEnemy(12, 4,  0 , -45, -10,-39,  10,-50);
        InitEnemy(13, 3,  25, -45,  20,-40,  40,-50);
        InitEnemy(14, 5,  40, -20,  30,-10,  50,-30);
        return usedEnemy;
    }

    public void InitBaby(float x, float z, float angle){
        Vector3 pos = new Vector3(x, 0 ,z);
        baby = Instantiate(Resources.Load<GameObject>("Prefabs/Baby"));
        baby.transform.position = pos;
        baby.transform.rotation = Quaternion.Euler(0.0f, angle ,0.0f);
        usedBaby.Add(baby);
    }

    public List<GameObject> getBabies()
    {
        InitBaby(-47 ,  47,  135);
        InitBaby(2.7f,  43, -135);
        InitBaby( 47 ,  47, -135);
        InitBaby( 45 ,  25, -90 );
        InitBaby(  0 , -20, -180);
        InitBaby(-47 , -47,  45 );
        InitBaby(-47 , -47,  45 );
        InitBaby( 47 , -47, -45 );
        return usedBaby;
    }

    public void stopEnemies(){
        int size = usedEnemy.Count;
        for(int i=0; i<size; i++){
            usedEnemy[i].gameObject.GetComponent<Animator>().SetBool("isWalk",false);
        }
    }
}
