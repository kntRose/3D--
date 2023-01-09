using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilpriestController{ 
    public GameObject devilpriest;
    private int type;       //角色类型：0为恶魔，1为牧师
    private int index;      //角色编号：0 1 2为恶魔，3 4 5为牧师
    private int state;      //角色状态：0在船上，1在左岸，2在右岸
    public Move move;

    public DevilpriestController(string t, int i){
        if(t == "Devil"){
            type = 0;
        } 
        else if(t == "Priest"){
            type = 1;
        } 
        index = i;
        state = 1;
    }

    public int getState(){
        return state;
    }

    public int getCharacterIndex(){
        return this.index;
    }

    public int getType(){
        return this.type;
    }

    public void setName(string n){
        this.devilpriest.name = n;
    }

    public void setPositionOnLeftBank(){
        this.state = 1;
        move.setDestination(new Vector3(-20 + index * 2, 2f, 0));
    }

    public void setPositionOnRightBank(){
        this.state = 2;
        move.setDestination(new Vector3(10 + index * 2, 2f, 0));
    }

    public void setPositionOnBoat(Vector3 pos){
        this.state = 0;
        move.setDestination(pos);
    }

    public void moveBoat(int boatState, int posOnBoat){
        if(boatState == 1){
            Vector3 target = devilpriest.transform.position + new Vector3(8, 0, 0);
            move.setDestination(target);
        }
        else if(boatState == 2){
            Vector3 target = devilpriest.transform.position + new Vector3(-8, 0, 0);
            move.setDestination(target);
        }
    }

    public void reset(){
        devilpriest.transform.position = new Vector3(-20f + index * 2, 2f, 0);
        move.reset();
    }
}
