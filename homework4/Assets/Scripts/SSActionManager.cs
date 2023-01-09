using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour { 
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingToAdd = new List<SSAction>();
    private List<int> watingToDelete = new List<int>();

    protected void Update(){
        foreach(SSAction ac in waitingToAdd){
            actions[ac.GetInstanceID()] = ac;
        }
        waitingToAdd.Clear();

        foreach(KeyValuePair<int, SSAction> kv in actions){
            SSAction ac = kv.Value;
            if(ac.destroy){
                watingToDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable){
                ac.Update();
            }
        }

        foreach(int key in watingToDelete){
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        watingToDelete.Clear();
    }
    public void RunAction(GameObject gameObject, SSAction action, IActionCallback manager){
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingToAdd.Add(action);
        action.Start();
    }
}