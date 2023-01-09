using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilpriestController{ 
    public GameObject devilpriest;
    private int type;       //角色类型：0为恶魔，1为牧师
    private int index;      //角色编号：0 1 2为恶魔，3 4 5为牧师
    private int state;      //角色状态：0在船上，1在左岸，2在右岸
    //public Move move;
    public float moveSpeed = 20;

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
       // move.setDestination(new Vector3(-20 + index * 2, 2f, 0));
    }

    public Vector3 getPosOnLeftBank(){
        return new Vector3(-20 + index * 2, 2f, 0);
    }

    public void setPositionOnRightBank(){
        this.state = 2;
        //move.setDestination(new Vector3(10 + index * 2, 2f, 0));
    }

    public Vector3 getPosOnRightBank(){
        return new Vector3(10 + index * 2, 2f, 0);
    }

    public void setPositionOnBoat(){
        this.state = 0;
        //move.setDestination(pos);
    }

    public Vector3 getDestinationOnBoat(int boatState, int posOnBoat){
        if(boatState == 1){
            return devilpriest.transform.position + new Vector3(8, 0, 0);
        }
        else if(boatState == 2){
            return devilpriest.transform.position - new Vector3(8, 0, 0);
        }
        return Vector3.zero;
    }

    public Vector3 getPos(){
        return this.devilpriest.transform.position;
    }

    public GameObject getGameobj(){
        return this.devilpriest;
    }

    public void reset(){
        devilpriest.transform.position = new Vector3(-20f + index * 2, 2f, 0);
        //move.reset();
    }
}
