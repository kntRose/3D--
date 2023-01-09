using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour{
    //巡逻兵碰撞触发器
    void OnCollisionEnter(Collision collision){
        //巡逻兵与玩家碰撞，进行攻击
        if(collision.gameObject.tag == "Player"){
            this.GetComponent<Animator>().SetTrigger("attack");
            Singleton<GameEventManager>.Instance.PlayerCatched();
        } 
        //巡逻兵与墙碰撞
        else{
            this.GetComponent<PatrolData>().isCollided = true;
        }
    }
}