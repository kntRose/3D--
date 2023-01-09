using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour{
    private IUserAction action;
    private ISceneController controller;
    GUIStyle buttonStyle;
    GUIStyle style;

    void Start(){
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 15;
        buttonStyle.normal.textColor = Color.white;
        style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.red;
    }

    private void Update(){
        action = SSDirector.getInstance().CurrentSceneController as IUserAction;
        controller = SSDirector.getInstance().CurrentSceneController as ISceneController;
        if(controller.getState().Equals(State.running)){
            //获取键盘输入，玩家依据输入进行移动
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            action.PlayerMove(x, y);
        }
    }

    private void OnGUI(){
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 300, 100, 50), "Score: " + controller.getScore().ToString(), style);

        controller = SSDirector.getInstance().CurrentSceneController as ISceneController;
        string buttonText = "";
        if(controller.getState().Equals(State.start) || controller.getState().Equals(State.pause)){
            buttonText = "Start";
        }
        if(controller.getState().Equals(State.lose)){
            buttonText = "Restart";
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 100, 200, 50), "You Lose", style);
        }
        if(controller.getState().Equals(State.running)){
            buttonText = "Pause";
        }

        if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 150, 100, 50), buttonText, buttonStyle)){
            if(buttonText == "Start"){
                controller.Begin();
            } 
            else if(buttonText == "Pause"){
                controller.Pause();
            } 
            else if(buttonText == "Restart"){
               controller.Restart();
            }
        }
    }
}