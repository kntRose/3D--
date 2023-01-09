using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionManager : SSActionManager, ISSActionCallback{
    public PatrolMoveAction move;
    public PatrolFollowAction follow;

    //巡逻兵移动
    public void PatrolMove(GameObject ptrl){
        this.move = PatrolMoveAction.GetAction(ptrl.transform.position);
        this.RunAction(ptrl, move, this);
    }

    //巡逻兵追击
    public void PatrolFollow(GameObject player, GameObject patrol){
        this.follow = PatrolFollowAction.GetAction(player);
        this.RunAction(patrol, follow, this);
    }

    //巡逻兵停止
    public void PatrolStop(){
        Stop();
    }

    public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null){}
}