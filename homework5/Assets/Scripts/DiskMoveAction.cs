using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMoveAction : SSAction{
    private Vector3 InitialSpeed;                        //飞碟初速度
    private Vector3 AcceleratedSpeed = Vector3.zero;     //飞碟加速度
    private float time;                                  
    private Vector3 angle = Vector3.zero;                //飞碟角度
    private DiskMoveAction(){}

    //获得飞碟移动初速度和角度
    public static DiskMoveAction GetSSAction(float angle, float power){ 
        DiskMoveAction DiskMove = CreateInstance<DiskMoveAction>();
        DiskMove.InitialSpeed = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        return DiskMove;
    }

    public override void Update(){
        time += Time.fixedDeltaTime;
        AcceleratedSpeed.y = 0; 
        transform.position += (InitialSpeed + AcceleratedSpeed) * Time.fixedDeltaTime;
        angle.z = Mathf.Atan((InitialSpeed.y + AcceleratedSpeed.y) / InitialSpeed.x) * Mathf.Rad2Deg;
        transform.eulerAngles = angle;

        //飞碟飞出画面，消除飞碟
        if(this.transform.position.y < -10 || this.transform.position.y > 10){
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

    public override void Start(){}
}
