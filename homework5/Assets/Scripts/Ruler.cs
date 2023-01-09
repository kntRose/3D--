using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Ruler{     
    //设置飞碟的各种属性
    public void setDisk(GameObject disk, int round){
        disk.transform.position = this.setPosition();
        disk.GetComponent<Renderer>().material.color = setColor();
        disk.transform.localScale = setScale(round);
        disk.GetComponent<Disk>().angle = setAngle();
        disk.GetComponent<Disk>().power = setPower(round);
    }

    //设置飞碟初始位置
    public Vector3 setPosition(){
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-1f, 5f);
        float z = Random.Range(-3f, 3f);
        return new Vector3(x, y, z);
    }

    //设置飞碟颜色RGB
    public Vector4 setColor(){
        int r = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        int g = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        int b = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        return new Vector4(r, g, b, 1);
    }

    //设置飞碟形状大小，回合越大，形状越小
    public Vector3 setScale(int round){
        float x = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        float y = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        float z = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        return new Vector3(x, y, z);
    }

    //设置飞碟角度
    public float setAngle(){
        return Random.Range(-360f, 360f);
    }
    
    //设置飞碟初速度倍率，回合越大，速度越快
    public float setPower(int round){
        return round;
    }

    //设置该轮飞碟发射时间间隔，回合越大，时间间隔越小
    public float setInterval(int round){
        return (float)(2 - 0.2 * round);
    }

    //获取这轮的目标分数，回合越大，目标分数要求越高
    public int getTarget(int round){
        if(round != -1){
            return 5 + round > 10 ? 10 : 5 + round;
        }
        return 0;
    }

    //判断是否进入能够下一轮
    public bool IfNextRound(int round,int score){
        if(round != -1 && score >= (5 + round > 10 ? 10 : 5 + round)){
            return true;
        }
        return false;
    }
}