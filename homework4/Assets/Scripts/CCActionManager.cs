using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager,IActionCallback{
    public FirstController sceneController;

    protected new void Start(){
        sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
    }

    public void moveBoat(BoatController boat){
        CCMoveToAction action = CCMoveToAction.getAction(boat.getDestination(), boat.moveSpeed);
        this.RunAction(boat.getGameobj(), action, this);
        SSDirector.getInstance().moving = true;
    }

    public void moveDevilpriest(DevilpriestController characterCtrl, Vector3 destination){
        Vector3 currentPos = characterCtrl.getPos();
        Vector3 middlePos = currentPos;

        if(destination.y > currentPos.y){       
            middlePos.y = destination.y;
        }
        else{   
            middlePos.x = destination.x;
        }
        
        SSAction action1 = CCMoveToAction.getAction(middlePos, characterCtrl.moveSpeed);
        SSAction action2 = CCMoveToAction.getAction(destination, characterCtrl.moveSpeed);
        SSAction seqAction = CCSequenceAction.getAction(1, 0, new List<SSAction> { action1, action2 });
        this.RunAction(characterCtrl.getGameobj(), seqAction, this);
        SSDirector.getInstance().moving = true;
    }

    public void ActionEvent(SSAction source, ActionEventType events = ActionEventType.Competeted){
        if(events == ActionEventType.Competeted){
            SSDirector.getInstance().moving = false; 
        }
        else{
            SSDirector.getInstance().moving = true;
        }
    }
}
