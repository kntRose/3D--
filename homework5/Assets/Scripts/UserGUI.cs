using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour{
    private UserAction action;
    public int round = 0;
    public int score = 0;
    public int target = 0;
    GUIStyle fontStyle = new GUIStyle();
    private int lastround = 0;

    void Start(){
        action = SSDirector.GetInstance().currentSceneController as UserAction;
    }

    private void Update(){
        if(Input.GetButtonDown("Fire1")){
            Vector3 position = Input.mousePosition;
            action.Hit(position);
        }
    }

    void OnGUI(){
        fontStyle.normal.textColor = new Color(255,0,0);   
        fontStyle.fontSize = 30;

        GUI.Label(new Rect(Screen.width / 2 - 40, 50, 180, 50), "score: " + score.ToString(), fontStyle);
        GUI.Label(new Rect(Screen.width / 2 + 80, 50, 180, 50), "target: " + target.ToString(), fontStyle);

        if(round != -1){
            GUI.Label(new Rect(Screen.width / 2 - 170, 50, 100, 50), "round: " + round.ToString(), fontStyle);
            lastround = round;
        }

        if(round == -1){
            GUI.Label(new Rect(Screen.width / 2 - 170, 50, 100, 50), "round: " + lastround.ToString(), fontStyle);
            GUI.Label(new Rect(Screen.width / 2 - 60, 300, 100, 50), "You Lose!", fontStyle);
        }

        if(GUI.Button(new Rect(Screen.width / 2 - 40, 120, 70, 30), "Restart", fontStyle)){
            action.restart();
        }
    }
}