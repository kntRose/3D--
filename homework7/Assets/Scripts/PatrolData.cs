using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour{
    public bool isInRange;        //玩家是否在巡逻兵范围内
    public bool isCollided;       //巡逻兵是否与物体发生碰
    public bool isFollowing;      //巡逻兵是否在追击玩家
    public int playerArea;        //玩家的区域
    public int patrolArea;        //巡逻兵的区域
    public GameObject player;     
}