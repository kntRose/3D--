using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController : MonoBehaviour, ISceneController, IUserAction{
    public UserGUI userGUI;
    public ScoreBoard scoreBoard;
    public int playerArea;
    private State state;
    public GameObject player;
    private List<GameObject> patrols;
    public PatrolActionManager patrolActionManager;
    public PatrolFactory patrolFactory;

    void Start(){
        SSDirector director = SSDirector.getInstance();
        director.CurrentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        scoreBoard = Singleton<ScoreBoard>.Instance;
        playerArea = 4;
        state = State.start;
        patrolActionManager = gameObject.AddComponent<PatrolActionManager>();
        patrolFactory = Singleton<PatrolFactory>.Instance;
        LoadResources();
    }

    //加载玩家和巡逻兵，摄像机追随玩家移动
    public void LoadResources(){
        player = Instantiate(Resources.Load<GameObject>("prefabs/player"), new Vector3(0, 0, -5f), Quaternion.identity);
        player.GetComponent<Animator>().SetBool("death", false);
        player.GetComponent<Animator>().SetBool("pause", true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().player = player;
        patrols = patrolFactory.CreatPatrols();
        for(int i = 0; i < patrols.Count; ++i){
            patrolActionManager.PatrolMove(patrols[i]);
        }
    }

    //更新玩家所在区域
    void Update(){
        for (int i = 0; i < patrols.Count; i++){
            patrols[i].GetComponent<PatrolData>().playerArea = playerArea;
        }
    }

    //玩家移动
    public void PlayerMove(float x, float y){
        if(x != 0 || y != 0){
            player.GetComponent<Animator>().SetBool("pause", false);
        } 
        else{
            player.GetComponent<Animator>().SetBool("pause", true);
        }
        x *= Time.deltaTime;
        y *= Time.deltaTime;

        player.transform.LookAt(new Vector3(player.transform.position.x + x, player.transform.position.y, player.transform.position.z + y));
        if(x == 0){
            player.transform.Translate(0, 0, Mathf.Abs(y) * 5);
        }
        else if(y == 0){
            player.transform.Translate(0, 0, Mathf.Abs(x) * 5);
        }
        else{
            player.transform.Translate(0, 0, (Mathf.Abs(y) + Mathf.Abs(x)) * 2.5f);
        }
    }

     //订阅者
    void OnEnable(){
        GameEventManager.OnEscapeAction += OnEscape;
        GameEventManager.OnFollowAction += OnFollow;
        GameEventManager.OnCatchAction += OnCatch;
    }

    void OnDisable(){
        GameEventManager.OnEscapeAction -= OnEscape;
        GameEventManager.OnFollowAction -= OnFollow;
        GameEventManager.OnCatchAction -= OnCatch;
    }

    public void OnEscape(GameObject patrol){
        patrolActionManager.PatrolMove(patrol);
        scoreBoard.Record();
    }

    public void OnFollow(GameObject patrol){
        patrolActionManager.PatrolFollow(player, patrol);
    }

    public void OnCatch(){
        state = State.lose;
        StopAllCoroutines();
        patrolFactory.PatrolPause();
        player.GetComponent<Animator>().SetTrigger("death");
        patrolActionManager.PatrolStop();
    }

    public int getScore(){
        return scoreBoard.score;
    }
    
    public State getState(){
        return state;
    }

    public void Begin(){
        state = State.running;
        patrolFactory.PatrolStart();
        player.GetComponent<Animator>().SetBool("pause", false);
    }

    public void Pause(){
        state = State.pause;
        patrolFactory.PatrolPause();
        player.GetComponent<Animator>().SetBool("pause", true);
        StopAllCoroutines();
    }

    public void Restart(){
        SceneManager.LoadScene("main");
    }
}
