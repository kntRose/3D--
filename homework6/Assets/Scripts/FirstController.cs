using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, UserAction{
    public int round;       //当前回合
    public int trial;       //机会次数
    public float interval;  //发射间隔
    public int score;       //当前分数
    public Ruler ruler;     //设置飞碟属性，判断是否获胜进入下一回合
    public UserGUI userGUI;
    private Queue<GameObject> DiskQueue = new Queue<GameObject>();
    public IActionManager ActionManager;   //设置飞碟运动
    public ScoreBoard scoreboard;   //计分板，获取分数
    public DiskFactory diskFactory;  //飞碟工厂，用于回收飞碟

    void Awake(){
        SSDirector director = SSDirector.GetInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    //加载资源
    public void LoadResources(){
        ActionManager = gameObject.AddComponent<PhysicalActionManager>() as IActionManager;
        this.gameObject.AddComponent<DiskFactory>();
        ruler = new Ruler();
        scoreboard = new ScoreBoard();
        diskFactory = Singleton<DiskFactory>.Instance;
    }

    //初始化
    void Start(){
        round = 1;
        interval = 0;
        trial = 0;
        EnterDiskQueue();
        userGUI.target = ruler.getTarget(round);
    }

    void Update(){
        //判断是否进入下一回合
        if(round != -1 && ruler.IfNextRound(round, scoreboard.score)){
            round++;
            trial = 0;
            EnterDiskQueue();
            userGUI.score = this.score = 0;
            scoreboard.reset();
            userGUI.target = ruler.getTarget(round);
        }
        //没进入下一回合，并且机会次数用完，游戏失败
        else if(round != -1 && !ruler.IfNextRound(round, scoreboard.score) && trial == 11){
            round = -1;
        }
        
        //飞碟按间隔发射
        if(this.round >= 1){
            if(interval > ruler.setInterval(round)){
                if(trial < 10){
                    DiskMove();
                    interval = 0;
                    trial++;
                }
                else if(trial == 10){
                    trial++;
                }
            }
            else{
                interval += Time.deltaTime;
            }
        }
        userGUI.round = this.round;
    }

    //设置当前飞碟属性，并且发射飞碟
    public void DiskMove(){
        if(DiskQueue.Count != 0){
            GameObject disk = DiskQueue.Dequeue();
            diskFactory.removeDisk(disk);
            ruler.setDisk(disk, round);
            disk.SetActive(true);
            ActionManager.DiskMove(disk, disk.GetComponent<Disk>().angle, disk.GetComponent<Disk>().power);
        }
    }

    //飞碟队列
    public void EnterDiskQueue(){
        int num = 10;
        for(int i = 0; i < num; i++){
            GameObject Disk = diskFactory.getDisk();
            DiskQueue.Enqueue(Disk);
        }
    }

    //判断点击飞碟，更新分数
    public void Hit(Vector3 position){
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for(int i = 0; i < hits.Length; i++){
            RaycastHit hit = hits[i];
            if(hit.collider.gameObject.GetComponent<Disk>() != null){
                hit.collider.gameObject.SetActive(false);
                scoreboard.getScore(hit.collider.gameObject);
                userGUI.score = scoreboard.score;
            }
        }
    }

    //重启游戏
    public void restart(){
        round = 1;
        userGUI.round = round;
        score = 0;
        userGUI.score = score;
        scoreboard.reset();
        interval = 0;
        trial = 0;
        EnterDiskQueue();
        userGUI.target = ruler.getTarget(round);
    }
}