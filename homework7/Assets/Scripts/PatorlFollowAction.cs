using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction{
    private float speed = 1.5f;          
    private GameObject player;           
    private PatrolData data;             

    public static PatrolFollowAction GetAction(GameObject player){
        PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
        action.enable = true;
        action.player = player;
        return action;
    }

    public override void Start(){
        data = this.gameobject.GetComponent<PatrolData>();
    }

    public override void Update(){
        //巡逻兵追击玩家
        if(SSDirector.getInstance().CurrentSceneController.getState().Equals(State.running)){
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            this.transform.LookAt(player.transform.position);
            if(data.isFollowing && (!(data.isInRange && data.patrolArea == data.playerArea) || data.isCollided)){
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
                this.gameobject.GetComponent<PatrolData>().isFollowing = false;
                Singleton<GameEventManager>.Instance.PlayerEscape(this.gameobject);
            }
        }
    }
}