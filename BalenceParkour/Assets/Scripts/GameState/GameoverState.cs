using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverState : FSMState
{

    public GameoverState(FSMManager fsm) : base(fsm) { }
    public override void OnEnter()
    {
        UIManager.instance.gameoverWindows.SetActive(true);
        if (UIManager.instance.score >= 1000)
            UIManager.instance.GameoverText.text = "You got it!!!";
        if (UIManager.instance.score > PlayerPrefs.GetFloat("Score"))
            PlayerPrefs.SetFloat("Score", UIManager.instance.score);
        UIManager.instance.finalScore.text = "Score: " + ((int)UIManager.instance.score).ToString() + "\n" + "\n" + "Best Score: " + ((int)PlayerPrefs.GetFloat("Score")).ToString();
    }
    public override void OnUpdate()
    {
    }
    public override void Check()
    {
        if (GameState.instance.gameStart)
            FSM.ChangeState(FSMManager.StateID.Start);
    }
    public override void OnExit()
    {
        UIManager.instance.gameoverWindows.SetActive(false);
    }
}

