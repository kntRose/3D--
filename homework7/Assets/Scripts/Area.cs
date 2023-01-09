using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour{
    public int area;
    FirstController sceneController;

    //地图触发器，检测玩家是否进入该区域
    void OnTriggerEnter(Collider collider){
        sceneController = SSDirector.getInstance().CurrentSceneController as FirstController;
        if(collider.gameObject.tag == "Player"){
            sceneController.playerArea = area;
        }
        if(collider.gameObject.tag == "Patrol"){
            collider.gameObject.GetComponent<PatrolData>().patrolArea = area;
        }
    }

    //地图触发器，检测巡逻兵是否离开该区域
    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "Patrol"){
            collider.gameObject.GetComponent<PatrolData>().isCollided = true;
        }
    }
}