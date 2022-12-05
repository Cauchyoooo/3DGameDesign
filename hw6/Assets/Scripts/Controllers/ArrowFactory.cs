using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    public GameObject arrow = null;
    List<GameObject> used;
    Queue<GameObject> free;


    void Start()
    {
        used = new List<GameObject>();
        free = new Queue<GameObject>();
        
    }

    public GameObject GetArrow(){
        if(free.Count == 0){
            arrow = Instantiate(Resources.Load<GameObject>("Prefabs/arrow"));
        }
        else{
            arrow = free.Dequeue();
            arrow.gameObject.SetActive(true);
            arrow.gameObject.tag = "Untagged";
            arrow.gameObject.transform.Find("head").gameObject.SetActive(true);
            arrow.gameObject.transform.Find("body").gameObject.SetActive(true);
            arrow.gameObject.transform.Find("head").GetComponent<arrowScript>().reset();
        }
        arrow.transform.position = new Vector3(0, 2.5f, -12.5F);
        used.Add(arrow);
        return arrow; 
    }


    public void FreeArrow(){
        if(used.Count<=10)
            return;
        for(int i=0; i<used.Count; i++){
            if(used[i].gameObject.transform.position.y <= -10 || used[i].gameObject.transform.position.z >= -5 || used[i].gameObject.tag == "Hit"){
                used[i].GetComponent<Rigidbody>().isKinematic = true;
                used[i].gameObject.SetActive(false);
                used[i].transform.position =  new Vector3 (0, 2.5f, -12.5f);
                free.Enqueue(used[i]);
                used.Remove(used[i]);
 
                break;
            }
        }
    }
}