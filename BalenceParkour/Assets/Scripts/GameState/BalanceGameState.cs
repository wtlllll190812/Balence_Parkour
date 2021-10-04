using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 平衡游戏状态
/// </summary>
public class BalanceGameState : FSMState
{
    public BalanceGameState(FSMManager fsm) : base(fsm) { }


    public override void OnEnter()
    {
        CameraManager.instance.targetFOV = 5;
        CameraManager.instance.targetPos = new Vector2(0, 0);
    }
    public override void OnUpdate()
    {
        MapManager.instance.moveSpeed += GameState.instance.acc * Time.deltaTime;
    }
    public override void Check()
    {
        // if (Input.GetKeyDown(KeyCode.C))
        //     FSM.ChangeState(FSMManager.StateID.NormalGame);
        if (GameState.instance.gameOver)
            FSM.ChangeState(FSMManager.StateID.Gameover);
    }
    public override void OnExit()
    {
        CameraManager.instance.targetFOV = 2;
        CameraManager.instance.targetPos = new Vector2(0, 0.25f);
    }

}
