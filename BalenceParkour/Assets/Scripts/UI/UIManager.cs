using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject playButton;//开始按钮
    public GameObject gameoverWindows;//结束界面 
    public Text GameoverText;
    public GameObject developer; //开发者
    public Text ScoreText;
    public float score;
    public Text finalScore;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!GameState.instance.gameOver)
        {
            score = MapManager.instance.levelNum * MapManager.instance.moveSpeed + Time.deltaTime - 6;
        }
        ScoreText.text = "Score: " + ((int)score).ToString();
    }
    public void GameStart()
    {
        GameState.instance.gameStart = true;
    }
    public void Developer()
    {
        playButton.SetActive(false);
        developer.SetActive(true);
    }
    public void Confirm()
    {
        playButton.SetActive(true);
        developer.SetActive(false);
    }
}
