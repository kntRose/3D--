using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMGUIHealthBar : MonoBehaviour{
    public float health = 0f;
    private Rect healthBar;
    public Rect increase;
    public Rect decrease;
    public Slider slider;

    void Start(){
        healthBar = new Rect(50, 50, 200, 30);
        increase = new Rect(75, 80, 40, 30);
        decrease = new Rect(175, 80, 40, 30);
    }

    void OnGUI(){
        if (GUI.Button(increase, "+")){
            health = health + 0.1f > 1f ? 1f : health + 0.1f;
        }
        if (GUI.Button(decrease, "-")){
            health = health - 0.1f < 0 ? 0 : health - 0.1f;
        }
        slider.value = health;
        GUI.HorizontalScrollbar(healthBar, 0f, health, 0f, 1f);
    }
}
