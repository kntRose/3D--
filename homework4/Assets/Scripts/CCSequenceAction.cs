using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSequenceAction : SSAction, IActionCallback{
    public List<SSAction> sequence;
    public int repeat = 1; 
    public int start = 0;

    public static CCSequenceAction getAction(int repeat, int start, List<SSAction> sequence){
        CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update(){
        if(sequence.Count == 0){
            return;  
        } 
        if(start < sequence.Count){
            sequence[start].Update();
        }
    }

    public void ActionEvent(SSAction source, ActionEventType events = ActionEventType.Competeted){
        source.destroy = false;
        this.start++;
        if(this.start >= sequence.Count){
            this.start = 0;
            if(repeat > 0){
                repeat--;    
            } 
            if(repeat == 0){
                this.destroy = true;
                this.callback.ActionEvent(this);
            }
        }
    }

    public override void Start(){
        foreach(SSAction action in sequence){
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }

    void OnDestroy(){
        foreach(SSAction action in sequence){
            Destroy(action);
        }
    }
}

