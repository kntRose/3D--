using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMoveAction : SSAction{
    private float pos_x, pos_z;                
    private bool turn = true;       //巡逻兵是否转弯
    private PatrolData data;                    

    public static PatrolMoveAction GetAction(Vector3 location){
        PatrolMoveAction action = CreateInstance<PatrolMoveAction>();
        action.enable = true;
        action.pos_x = location.x;
        action.pos_z = location.z;
        return action;
    }

    public override void Start(){
        data = this.gameobject.GetComponent<PatrolData>();
    }

    public override void Update(){
        if(SSDirector.getInstance().CurrentSceneController.getState().Equals(State.running)){
            //巡逻兵开始移动
            if(turn == true){
                pos_x = this.transform.position.x + Random.Range(-4f, 4f);
                pos_z = this.transform.position.z + Random.Range(-4f, 4f);
                this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
                this.gameobject.GetComponent<PatrolData>().isCollided = false;
                turn = false;
            }
            float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));
            if(this.gameobject.GetComponent<PatrolData>().isCollided){
                this.transform.Rotate(Vector3.up, 180);
                GameObject temp = new GameObject();
                temp.transform.position = this.transform.position;
                temp.transform.rotation = this.transform.rotation;
                temp.transform.Translate(0, 0, Random.Range(0.01f, 0.1f));
                pos_x = temp.transform.position.x;
                pos_z = temp.transform.position.z;
                this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
                this.gameobject.GetComponent<PatrolData>().isCollided = false;
                Destroy(temp);
            } 
            else if(distance <= 0.1){
                turn = true;
            } 
            else{
                this.transform.Translate(0, 0, Time.deltaTime);
            }
            
            //玩家在巡逻兵范围内，巡逻兵开始追随
            if(!data.isFollowing && data.isInRange && data.patrolArea == data.playerArea && !data.isCollided){
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
                this.gameobject.GetComponent<PatrolData>().isFollowing = true;
                Singleton<GameEventManager>.Instance.PlayerFollowed(this.gameobject);
            }
        }
    }
}