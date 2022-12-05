using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score;
    // public int arrowNum;

    public void Start(){
        score = 0;
        // arrowNum = 10;
    }
    // public int getScore(){return score;}
    public void Record(string ColliderTag){
        if(ColliderTag == "10"){
            score+=10;
        }
        else if(ColliderTag == "8"){
            score+=8;
        }
        else if(ColliderTag == "6"){
            score+=6;
        }
        else if(ColliderTag == "4"){
            score+=4;
        }
        else if(ColliderTag == "2"){
            score+=2;
        }
                
    }

    public void Reset(){
        score = 0;
        // arrowNum = 10;
    }
}
