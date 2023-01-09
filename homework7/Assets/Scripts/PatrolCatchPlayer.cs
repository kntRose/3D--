using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCatchPlayer : MonoBehaviour{
    //玩家在巡逻兵范围内触发器
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<PatrolData>().isInRange = true;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = collider.gameObject;
        }
    }

    //玩家在巡逻兵范围外触发器
    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<PatrolData>().isInRange = false;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }
}