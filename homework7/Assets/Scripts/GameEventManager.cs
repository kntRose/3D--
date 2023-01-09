using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//发布者
public class GameEventManager : MonoBehaviour{
    //玩家逃离巡逻兵
    public delegate void EscapeAction(GameObject patrol);
    public static event EscapeAction OnEscapeAction;
    public void PlayerEscape(GameObject patrol){
        if (OnEscapeAction != null) {
            OnEscapeAction(patrol);
        }
    }

    //巡逻兵追击玩家
    public delegate void FollowAction(GameObject patrol);
    public static event FollowAction OnFollowAction;
    public void PlayerFollowed(GameObject patrol){
        if (OnFollowAction != null) {
            OnFollowAction(patrol);
        }
    }

    //巡逻兵抓捕玩家
    public delegate void CatchAction();
    public static event CatchAction OnCatchAction;
    public void PlayerCatched(){
        if (OnCatchAction != null) {
            OnCatchAction();
        }
    }
}