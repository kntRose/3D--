using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour{
    public GameObject player;            //相机跟随的物体
    public float speed = 5f;             //相机移动速度
    Vector3 offset;                      //相机与物体相对偏移位置

    void Start(){
        offset = new Vector3(0f, 7f, -4f);
    }

    //摄像机跟随目标移动
    void FixedUpdate(){
        Vector3 target = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}