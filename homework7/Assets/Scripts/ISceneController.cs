using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController{
    void LoadResources();
    int getScore();              
    State getState();               
    void Begin();                     
    void Pause(); 
    void Restart();               
}