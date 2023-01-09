using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{
    public class SSDirector : System.Object{
        private static SSDirector _instance;
        public ISceneController currentSceneController{ get; set; }
        public bool running{ get; set; }

        public static SSDirector GetInstance(){
            if(_instance == null){
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public interface ISceneController{
        void LoadResources();
    }

    public interface UserAction{
        void Hit(Vector3 pos);
        void restart();
    }

    public enum SSActionEventType : int { Started, Completed }

    public interface ISSActionCallback{
        void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed);
    }
}