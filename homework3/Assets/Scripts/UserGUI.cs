using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour{   
    private IUserAction action;
    public int gameState = 0;  //游戏状态：0为进行中，1为赢，-1为输
    public bool click = true;

    void Start(){
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }
 
    void OnGUI(){
        if(gameState == 1){
            GUI.Label(new Rect(Screen.width / 2 - 40, 150, 100, 50), "Win!");
        }
        else if(gameState == -1){
            GUI.Label(new Rect(Screen.width / 2 - 25, 150, 100, 50), "Lose!");
        }
        
        if(GUI.Button(new Rect(Screen.width / 2 - 40, 200, 70, 30), "Restart")){
            action.restart();
        }
    }

    void OnMouseDown(){
        if(click && !SSDirector.getInstance().moving){
            if(gameObject.name == "boat"){
                action.moveBoat();
            }
            else{
                string[] name = { "Devil", "Priest" };
                int index = gameObject.name[gameObject.name.Length - 1] - '0';
                for(int i = 0; i < 2; i++){
                    if(gameObject.name.Substring(0, gameObject.name.Length - 1) == name[i]){
                        index += 3 * i;
                    }
                }
                action.moveDevilpriest(index);
            }
        }
    }
}
