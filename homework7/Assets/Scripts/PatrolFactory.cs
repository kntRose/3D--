using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour{
    public GameObject patrol = null;
    private List<PatrolData> UsingPatrols = new List<PatrolData>(); 

    //创建巡逻兵
    public List<GameObject> CreatPatrols(){
        List<GameObject> patrols = new List<GameObject>();
        float[] pos_x = {-5f, 5f};
        float[] pos_z = {5f, -5f};
        //在四个区域分别创建一个巡逻兵
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                patrol = Instantiate(Resources.Load<GameObject>("prefabs/patrol"));
                patrol.transform.position = new Vector3(pos_x[j], 0, pos_z[i]);
                patrol.GetComponent<PatrolData>().patrolArea = i * 2 + j + 1;
                patrol.GetComponent<PatrolData>().playerArea = 4;
                patrol.GetComponent<PatrolData>().isInRange = false;
                patrol.GetComponent<PatrolData>().isFollowing = false;
                patrol.GetComponent<PatrolData>().isCollided = false;
                patrol.GetComponent<Animator>().SetBool("pause", true);
                UsingPatrols.Add(patrol.GetComponent<PatrolData>());
                patrols.Add(patrol);
            }
        }
        return patrols;
    }

    //设置动画条件变量，巡逻兵开始移动
    public void PatrolStart(){
        for (int i = 0; i < UsingPatrols.Count; i++){
            UsingPatrols[i].gameObject.GetComponent<Animator>().SetBool("pause", false);
        }
    }

    //设置动画条件变量，巡逻兵暂停移动
    public void PatrolPause(){
        for (int i = 0; i < UsingPatrols.Count; i++){
            UsingPatrols[i].gameObject.GetComponent<Animator>().SetBool("pause", true);
        }
    }
}