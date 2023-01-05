using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType:int {Started, Completed}
public interface ISSActionCallback
{
    void SSActionEvent(
        SSAction source, 
        GameObject ball = null
    );

}
