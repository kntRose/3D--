using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController{ 
    public GameObject boat;
    private int boatState;       //1船在左边，2船在右边
    private int[] indexOnBoat;   //记录船上人员的编号，-1为空
    public Move move;

    public BoatController(){
        boatState = 1;
        indexOnBoat = new int[2];
        for(int i = 0; i < 2; i++){
            indexOnBoat[i] = -1;
        }
    }

    public void setName(string n){
        this.boat.name = n;
    }

    public int getState(){
        return this.boatState;
    }

    public int Empty_num(){
        int counter = 0;
        for(int i = 0; i < 2; i++){
            if(indexOnBoat[i] == -1)
                counter++;
        }
        return counter;
    }

    //返回船上为空的位置
    public Vector3 Empty_Position(){
        for(int i = 0; i < 2; i++){
            if(indexOnBoat[i] == -1){
                if(boatState == 1){
                    return new Vector3((-5f + 2 * i), -0.5f, 0);
                } 
                else if(boatState == 2){
                    return new Vector3((3f + 2 * i), -0.5f, 0);
                }
                break;
            }
        }
        return Vector3.zero;
    }

    public void put(DevilpriestController devilpriest){
        for(int i = 0; i < 2; i++){
            if(indexOnBoat[i] == -1){ 
                indexOnBoat[i] = devilpriest.getCharacterIndex();
                break;
            }
        }
    }

    public void remove(DevilpriestController devilpriest){
        for(int i = 0; i < 2; i++){
            if(indexOnBoat[i] == devilpriest.getCharacterIndex()){
                indexOnBoat[i] = -1;
                break;
            }
        }
    }

    public void moveToBank(){
        if(boatState == 1){
            Vector3 target = boat.transform.position + new Vector3(8, 0, 0);
            move.setDestination(target);
            this.boatState = 2;
        }
        else if(boatState == 2){
            Vector3 target = boat.transform.position - new Vector3(8, 0, 0);
            move.setDestination(target);
            this.boatState = 1;
        }
    }

    //返回船上人员的编号
    public int[] Boat_person(){
        int[] result = new int[2];
        for(int i = 0; i < 2; i++){
            result[i] = indexOnBoat[i];
        }
        return result;
    }

    public void reset(){
        boatState = 1;
        for(int i = 0; i < 2; i++){
            indexOnBoat[i] = -1;
        }
        boat.transform.position = new Vector3(-4, -2, 0);
        move.reset();
    }
}
