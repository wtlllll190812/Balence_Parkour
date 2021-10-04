using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态机管理类
/// </summary>
public class FSMManager
{
    private Dictionary<StateID, FSMState> m_StateMap = new Dictionary<StateID, FSMState>();

    /// <summary>
    /// 确定状态机所属物体
    /// </summary>
    /// <param name="own"></param>
    public FSMManager(GameObject own) { Owner = own; }

    /// <summary>
    /// 状态机所属物体
    /// </summary>
    public GameObject Owner { get; private set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public FSMState CurrentState { get; private set; }

    /// <summary>
    /// 当前状态ID
    /// </summary>
    public StateID CurrentStateID { get; private set; }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="id">状态ID</param>
    /// <param name="state">状态对象</param>
    public void AddState(StateID id, FSMState state)
    {
        if (CurrentState == null)
        {
            CurrentStateID = id;
            CurrentState = state;
            // CurrentState.OnEnter();
        }
        if (m_StateMap.ContainsKey(id))
        {
            return;
        }
        m_StateMap.Add(id, state);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="id">状态的编号</param>
    public void ChangeState(StateID id)
    {
        if (id == CurrentStateID) return;
        if (m_StateMap.ContainsKey(id))
        {
            CurrentState.OnExit();
            CurrentStateID = id;
            CurrentState = m_StateMap[CurrentStateID];
            CurrentState.OnEnter();
        }
        else
            Debug.LogError("不存在该状态或该状态未添加");
    }

    /// <summary>
    /// 此处调用了状态的刷新和检查方法
    /// </summary>
    public void OnUpdate()
    {
        CurrentState.Check();
        CurrentState.OnUpdate();
    }

    /// <summary>
    /// 标识状态ID的枚举
    /// </summary>
    public enum StateID
    {
        Start,
        NormalGame,
        BalanceGame,
        Gameover
    }
}
