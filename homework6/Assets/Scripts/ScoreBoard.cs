using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard{
    public int score;

    //获取当前得分，当飞碟颜色为红绿蓝时，分数为2
    public void getScore(GameObject disk){
        int i = 1;
        if(disk.GetComponent<Renderer>().material.color == new Color(255, 0, 0, 1)){
           i += 1; 
        }
        else if(disk.GetComponent<Renderer>().material.color == new Color(0, 255, 0, 1)){
           i += 1; 
        }
        else if(disk.GetComponent<Renderer>().material.color == new Color(0, 0, 255, 1)){
           i += 1; 
        }
        score += i;
    }
    
    public void reset(){
        score = 0;
    }
}