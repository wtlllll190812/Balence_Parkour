using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartState : FSMState
{
    public StartState(FSMManager fsm) : base(fsm) { }

    public override void Check()
    {
        if (GameState.instance.gameStart)
            FSM.ChangeState(FSMManager.StateID.NormalGame);
    }
    public override void OnEnter()
    {
        if (GameState.instance.gameOver)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            UIManager.instance.playButton.SetActive(true);
            UIManager.instance.ScoreText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

    }
    public override void OnExit()
    {
        GameState.instance.gameStart = false;
        UIManager.instance.playButton.SetActive(false);
        UIManager.instance.ScoreText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
