using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机基类
/// </summary>
public abstract class FSMState : MonoBehaviour
{
    protected FSMManager FSM = null;
    protected FSMState(FSMManager fsm)
    {
        FSM = fsm;
    }
    /// <summary>
    /// 在刚进入该状态时调用
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 进入状态后使用
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 检测状态转换
    /// </summary>
    public virtual void Check() { }

    /// <summary>
    /// 离开该状态时使用
    /// </summary>
    public virtual void OnExit() { }
}
