using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType : int{Started, Competeted}

public enum State{running, pause, start, lose}  //游戏状态

public interface ISSActionCallback{
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null);
}