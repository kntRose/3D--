using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankController {
    public GameObject bank;
    private int index;          //左岸为0，右岸为1
    public int[] indexOnBank;   //记录在岸上人员的编号：-1为空，0为牧师，1为恶魔

    public BankController(string i){
        if(i == "left"){
            index = 0;
        } 
        else if(i == "right"){
            index = 1;
        } 

        indexOnBank = new int[6];
        for(int j = 0; j < 6; j++){
            indexOnBank[j] = -1;
        }
    }

    public void remove(DevilpriestController devilpriest){
        indexOnBank[devilpriest.getCharacterIndex()] = -1;
    }

    public void put(DevilpriestController devilpriest){
        indexOnBank[devilpriest.getCharacterIndex()] = devilpriest.getType();
    }

    public void setName(string n){
        this.bank.name = n;
    }
    
    //返回岸上恶魔数量
    public int Devil_num(){  
        int count = 0;
        for(int i = 0; i < 6; i++){
            if(indexOnBank[i] == 0){
                count++;
            } 
        }
        return count;
    }

    //返回岸上牧师数量
    public int Priest_num(){
        int count = 0;
        for(int i = 0; i < 6; i++){
            if(indexOnBank[i] == 1){
                count++;
            } 
        }
        return count;
    }

    public void reset(){
        if(index == 0){
            int j;
            for(j = 0; j < 3; j++){
                indexOnBank[j] = 0;
            }
            for(; j < 6; j++){
                indexOnBank[j] = 1;
            }
        }
        else if(index == 1){
            for(int j = 0; j < 6; j++){
                indexOnBank[j] = -1;
            }
        }
    }
}
