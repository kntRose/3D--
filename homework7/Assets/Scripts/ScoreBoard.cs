using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour{
    public int score;    //计分板

    void Start(){
        score = 0;
    }

    public void Record(){
        score++;
    }

    public void Reset(){
        score = 0;
    }
}
