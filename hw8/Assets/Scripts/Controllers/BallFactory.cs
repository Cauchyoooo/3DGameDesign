using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory: MonoBehaviour
{
    public GameObject ball = null;
    List<GameObject> used;
    Queue<GameObject> free;

    void Start()
    {
        used = new List<GameObject>();
        free = new Queue<GameObject>();
    }

    public GameObject GetBall()
    {
        if(free.Count == 0){
            ball = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"));
        }
        else{
            ball = free.Dequeue();
            ball.gameObject.SetActive(true);
            ball.gameObject.tag = "Untagged";
        }
        ball.transform.position = new Vector3(0, 10f, -45f);
        used.Add(ball);
        return ball;
    }

    public void FreeBall()
    {
        for(int i=0;i<used.Count;i++){
            if(used[i] == null){
                break;
            } 
            if(used[i].gameObject.tag == "Finish"){
                used[i].GetComponent<Rigidbody>().isKinematic = true;
                used[i].gameObject.SetActive(false);
                used[i].transform.position =  new Vector3 (0, 10f, -45f);
                used[i].transform.rotation = Quaternion.Euler(0,0,0);
                free.Enqueue(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}