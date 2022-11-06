using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void ClickCharacter(myCharacterController charactorController);
    void moveBoat();
    void Restart();  
}