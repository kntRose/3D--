using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback{
    private Dictionary<int, SSAction> Actions = new Dictionary<int, SSAction>();   
    private List<SSAction> AddQueue = new List<SSAction>();                
    private List<int> DeleteQueue = new List<int>();                                  

    protected void Update(){
        foreach(SSAction i in AddQueue){
            Actions[i.GetInstanceID()] = i;
        }
        AddQueue.Clear();

        foreach(KeyValuePair<int, SSAction> j in Actions){
            SSAction action = j.Value;
            if(action.destroy){
                DeleteQueue.Add(action.GetInstanceID());
            }
            else if(action.enable){
                action.Update();
            }
        }

        foreach(int k in DeleteQueue){
            SSAction action = Actions[k];
            Actions.Remove(k);
            Object.Destroy(action);
        }
        DeleteQueue.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager){
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        AddQueue.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed){}
}