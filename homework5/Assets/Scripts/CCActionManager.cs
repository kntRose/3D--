using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager{
    public DiskMoveAction move;                           
    public FirstController scene_controller;            

    protected void Start(){
        scene_controller = (FirstController)SSDirector.GetInstance().currentSceneController;
        scene_controller.ActionManager = this;
    }
    
    public void DiskMove(GameObject disk, float angle, float power){
        move = DiskMoveAction.GetSSAction(angle, power); 
        this.RunAction(disk, move, this);
    }
}