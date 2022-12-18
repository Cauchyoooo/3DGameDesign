using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType:int {Started, Completed}
public interface ISSActionCallback
{
    //回调函数
    void SSActionEvent(
        SSAction source,
        int intParam = 0,
        GameObject objectParam = null
    );
}