using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour{
    public GameObject Disk;
    private List<GameObject> UsingDisk = new List<GameObject>();   //正在使用的飞碟
    private List<GameObject> UsedDisk = new List<GameObject>();    //已经使用过的飞碟
    int Index;

    private void Awake(){
        Disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
        Disk.name = "prefab";
        Disk.AddComponent<Disk>();
        Disk.SetActive(false);
        Index = 0;
    }

    //获取新的飞碟
    public GameObject getDisk(){
        GameObject NewDisk = null;
        if(UsedDisk.Count > 0){
            NewDisk = UsedDisk[0].gameObject;
            UsedDisk.Remove(UsedDisk[0]);
            UsingDisk.Add(NewDisk);
            Debug.Log("uesdDisk " + NewDisk.name);
        }
        else{
            NewDisk = GameObject.Instantiate<GameObject>(Disk, Vector3.zero, Quaternion.identity);
            NewDisk.name = Index.ToString();
            UsingDisk.Add(NewDisk);
            Index++;
            Debug.Log("usingDisk " + NewDisk.name);
        }
        return NewDisk;
    }

    //回收使用过的飞碟
    public void removeDisk(GameObject obj){
        if(obj != null){
            obj.SetActive(false);
            UsingDisk.Remove(obj);
            UsedDisk.Add(obj);
        }
    }
}