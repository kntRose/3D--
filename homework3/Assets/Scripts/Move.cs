using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour{
    private float moveSpeed = 20;
    private int moveState;     //0为不移动, 1为移动到中间, 2为移动到目标
    private Vector3 middle;
    private Vector3 target;

    void Update(){
        if(moveState == 1){
            transform.position = Vector3.MoveTowards(transform.position, middle, moveSpeed * Time.deltaTime);
            if(transform.position == middle){
                moveState = 2;
            }
        }
        else if(moveState == 2){
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            if(transform.position == target){
                moveState = 0;
            }
        }
        SSDirector.getInstance().moving = false;
    }

    public void setDestination(Vector3 tar){
        SSDirector.getInstance().moving = true;
        target = tar;
        middle = tar;
        if(tar.y == transform.position.y){   
            moveState = 2;
        }
        else if(tar.y < transform.position.y){  
            middle.y = transform.position.y;
        }
        else{                            
            middle.x = transform.position.x;
        }
        moveState = 1;
    }

    public void reset(){
        moveState = 0;
    }
}
