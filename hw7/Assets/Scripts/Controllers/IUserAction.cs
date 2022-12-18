using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void movePlayer(float tran_x, float tran_z, bool isShift);
    int getScore();
    int getBabyNum();
    bool getGameOver();
    void Restart();
}
