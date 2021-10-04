using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 普通游戏状态
/// </summary>
public class NormalGameState : FSMState
{
    private float speedChangeState;//模式变化时速度

    protected Rigidbody2D balanceRb;//天平的刚体组件
    public NormalGameState(FSMManager fsm, Rigidbody2D balanceRb, float speedChangeState) : base(fsm)
    {
        this.balanceRb = balanceRb;
        this.speedChangeState = speedChangeState;
    }
    public override void OnEnter()
    {
        balanceRb.freezeRotation = true;
    }
    public override void OnUpdate()
    {
        MapManager.instance.moveSpeed += GameState.instance.acc * Time.deltaTime;
    }
    public override void Check()
    {
        if (MapManager.instance.moveSpeed >= speedChangeState)
        {
            FSM.ChangeState(FSMManager.StateID.BalanceGame);
        }
        if (GameState.instance.gameOver == true)
            FSM.ChangeState(FSMManager.StateID.Gameover);
    }
    public override void OnExit()
    {
        CameraManager.instance.shakeTime = 1;
        balanceRb.freezeRotation = false;
    }
}
