using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Init();
    void Shoot();
    void ChangeView(char c);
    bool IsBallNull();
}