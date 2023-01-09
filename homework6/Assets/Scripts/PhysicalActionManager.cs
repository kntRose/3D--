using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalActionManager : MonoBehaviour, IActionManager{
    public FirstController scene_controller;            

    protected void Start(){
        scene_controller = (FirstController)SSDirector.GetInstance().currentSceneController;
        scene_controller.ActionManager = this;
    }

    public void DiskMove(GameObject disk, float angle, float power){
        Vector3 vector = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power * 5;
        //飞碟设置刚体属性
        disk.GetComponent<Rigidbody>().velocity = vector;
        disk.GetComponent<Rigidbody>().useGravity = false;
    }
}