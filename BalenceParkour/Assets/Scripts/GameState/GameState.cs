using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏状态机管理
/// </summary>
public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Rigidbody2D balanceRb;//天平的刚体组件
    public float acc;//地图移动加速度
    public float speedChangeState;//模式变化时速度
    public bool gameStart;//游戏开始的标志位
    public bool gameOver;//游戏结束标志
    public Player player;//玩家
    public GameObject prefab;
    public List<GameObject> farmars = new List<GameObject>();
    public FSMManager GameFSM;//状态机对象
    private NormalGameState StateNG;//正常游戏状态
    private BalanceGameState StateBG;//平衡游戏状态
    private StartState StateST;//游戏开始状态
    private GameoverState StateGG;//游戏结束状态
    /// <summary>
    /// 状态机初始化
    /// </summary>
    private void FSMInit()
    {
        GameFSM = new FSMManager(gameObject);
        StateNG = new NormalGameState(GameFSM, balanceRb, speedChangeState);
        StateBG = new BalanceGameState(GameFSM);
        StateST = new StartState(GameFSM);
        StateGG = new GameoverState(GameFSM);

        GameFSM.AddState(FSMManager.StateID.Start, StateST);
        GameFSM.AddState(FSMManager.StateID.NormalGame, StateNG);
        GameFSM.AddState(FSMManager.StateID.BalanceGame, StateBG);
        GameFSM.AddState(FSMManager.StateID.Gameover, StateGG);
        GameFSM.ChangeState(FSMManager.StateID.Start);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        // DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        FSMInit();
    }
    void Update()
    {
        Debug.Log(gameOver);
        GameFSM.OnUpdate();
        Debug.Log(GameFSM.CurrentStateID);
    }
    public void AddLeftFarmar(Vector3 target, bool Sub)
    {
        GameObject farmar = Instantiate(prefab, target, Quaternion.identity);
        if (Sub)
            farmars.Add(farmar);
    }
}
