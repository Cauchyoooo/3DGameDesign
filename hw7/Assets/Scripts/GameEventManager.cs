using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //  分数变化
    public delegate void ScoreEvent();
    public static event ScoreEvent scoreChange;
    //  游戏结束
    public delegate void GameOverEvent();
    public static event GameOverEvent gameOverChange;
    //  Baby数量
    public delegate void BabyEvent();
    public static event BabyEvent babyChange;

    //  分数变化
    public void playerEscape()
    {
        if (scoreChange != null)
        {
            scoreChange();
        }
    }
    //  游戏结束
    public void playerGameOver()
    {
        if (gameOverChange != null)
        {
            gameOverChange();
        }
    }
    //  Baby数量
    public void reduceBabyNum()
    {
        if (babyChange != null)
        {
            babyChange();
        }
    }
}

