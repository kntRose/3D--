using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

    protected void Update(){
        foreach(SSAction action in waitingAdd){
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();
        foreach(KeyValuePair<int, SSAction> kv in actions){
            SSAction action = kv.Value;
            if(action.enable){
                action.Update(); // update action
            } 
            else if(action.destroy){
                waitingDelete.Add(action.GetInstanceID()); // release action
            }
        }
        foreach(int key in waitingDelete){
            SSAction action = actions[key];
            actions.Remove(key);
            Destroy(action);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback callback){
        action.gameobject = gameObject;
        action.transform = gameObject.transform;
        action.callback = callback;
        waitingAdd.Add(action);
        action.Start();
    }

    public void Stop(){
        foreach(KeyValuePair<int, SSAction> kv in actions){
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }

    protected void Start(){}
}